using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Entities.Shared.Reservations;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Interfaces.WorkSpaceReservations.Services;

namespace MOCA.Services.Implementation.WorkSpaceReservations
{
    public class WorkSpaceReservationsServiceCRM : IWorkSpaceReservationsServiceCRM
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;

        public WorkSpaceReservationsServiceCRM(IUnitOfWork unitOfWork, IMapper mapper, IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
        }

        public async Task<PagedResponse<IReadOnlyList<GetAllWorkSpaceReservationsResponse>>> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request)
        {
            var data = await _unitOfWork.WorkSpaceReservationsRepositoryCRM.GetAllWorkSpaceSubmissions(request);

            if (data.Count > 0)
            {
                return new PagedResponse<IReadOnlyList<GetAllWorkSpaceReservationsResponse>>(data, request.pageNumber, request.pageSize, data.Count);
            }
            return new PagedResponse<IReadOnlyList<GetAllWorkSpaceReservationsResponse>>(null, request.pageNumber, request.pageSize);
        }

        public async Task<PagedResponse<IReadOnlyList<GatFilteredWorkSpaceReservationResponse>>> GetFilteredSubmissions(GatFilteredWorkSpaceReservationDto request)
        {
            var data = await _unitOfWork.WorkSpaceReservationsRepositoryCRM.GetFilteredSubmissions(request);

            if (data.Count > 0)
            {
                return new PagedResponse<IReadOnlyList<GatFilteredWorkSpaceReservationResponse>>(data, request.pageNumber, request.pageSize, data.Count);

            }
            return new PagedResponse<IReadOnlyList<GatFilteredWorkSpaceReservationResponse>>(null, request.pageNumber, request.pageSize);
        }

        public async Task<Response<WorkSpaceReservationLocationsDropDown>> GetWorkSpaceLocationsDropDowns()
        {
            var locations = await _unitOfWork.WorkSpaceReservationsRepositoryCRM.GetWorkSpaceLocationsDropDowns();

            var workSpaceReservationLocations = new WorkSpaceReservationLocationsDropDown
            {
                Locations = locations
            };

            return new Response<WorkSpaceReservationLocationsDropDown>(workSpaceReservationLocations);
        }

        public async Task<Response<WorkSpaceReservationHistoryResponse>> GetWorkSpaceOpportunityInfoHistory(GetWorkSpaceReservationHistoryDto request)
        {
            throw new NotImplementedException();

            // From Id, and ReservationTypeId, Choose the right WorkSpaceReservation Tables depend on type id to get where same id
            // with include 1. BasicUser, 2. Location .. then Include LocationType,
            // 3. ReservationTransactions..then Include Reservation Details
            // 4. ReservationType, 5. Top Ups

            if (request.ReservationTypeId == 1)
            {
                return await GetReservationInfoHourly(request);
            }
            else if(request.ReservationTypeId == 2)
            {
                return await GetReservationInfoTailored(request);
            }
            else if(request.ReservationTypeId == 3)
            {
                return await GetReservationInfoBundle(request);
            }

            return new Response<WorkSpaceReservationHistoryResponse>("ReservationTypeId is not correct");
        }

        private async Task<Response<WorkSpaceReservationHistoryResponse>> GetReservationInfoHourly(GetWorkSpaceReservationHistoryDto request)
        {
            var reservation = await _unitOfWork.WorkSpaceReservationHourlyRepo
                                              .GetReservationInfo(request.WorkSpaceReservationId);

            if (reservation == null)
            {
                return new Response<WorkSpaceReservationHistoryResponse>("there's no such Reservation");
            }


            var reservationTransaction = await _unitOfWork.WorkSpaceReservationHourlyRepo
                                                      .GetRelatedReservationTransaction(request.WorkSpaceReservationId,
                                                                                        request.ReservationTypeId);

            // Get Entry Scan Time 

            var entryScanTime = reservationTransaction.ReservationDetails
                                                      .OrderByDescending(r => r.CreatedAt)
                                                      .FirstOrDefault().StartDateTime;

            // Get Reservation Status
            var status = GetReservationStatus(reservationTransaction);

            if (string.IsNullOrEmpty(status))
            {
                return new Response<WorkSpaceReservationHistoryResponse>("there's missing Reservation info");
            }

            // List of Foodics Details

            // Set top ups history, and Gifted Hours
            List<WorkSpaceTopupHistoryResponse> topupHistory = new List<WorkSpaceTopupHistoryResponse>();
            List<GiftedHours> giftedHours = new List<GiftedHours>();

            foreach (var item in reservation.TopUps)
            {
                if (item.Description == null)
                {
                    topupHistory.Add(new WorkSpaceTopupHistoryResponse
                    {
                        Id = item.Id,
                        Amount = item.HourlyTotalPrice,
                        CreatedAt = item.CreatedAt,
                        CreatedBy = item.CreatedBy,
                        Type = "Hourly",
                        //Hours = // from LoungeLocationPricing
                        Payment_Method = item.PaymentMethod.Name
                    });
                }
                else
                {
                    giftedHours.Add(new GiftedHours
                    {
                        Reason = item.Description,
                        AdminName = item.CreatedBy,
                        CreatedAt = item.CreatedAt,
                        //HoursAdded = // from LoungeLocationPricing, 
                    });
                }
            }

            var reservationInfo = new WorkSpaceReservationHistoryResponse
            {
                Id = reservation.Id,
                BasicUserId = reservation.BasicUserId,
                FirstName = reservation.BasicUser.FirstName,
                LastName = reservation.BasicUser.LastName,
                Amount = reservation.Price,
                PaymentMethod = reservation.PaymentMethodId,
                Status = status,
                Mode = reservation.TopUps.Count == 0 ? "Basic" : "TopUp",
                ReservationType = "Hourly",
                ReservationTypeId = 1,
                EntryScanTime = entryScanTime,
                lstGiftedHours = giftedHours,
                lstTopupHistory = topupHistory,
                OpportunityStartDate = reservation.CreatedAt,
                EndDate = reservationTransaction.ExtendExpiryDate,
                CreditHours = reservationTransaction.RemainingHours,
                CountryCode = reservation.BasicUser.Country.CountryCode,
                MobileNumber = reservation.BasicUser.MobileNumber,
                DateTime = reservation.Date,
                LocationId = reservation.LocationId,
                LocationName = reservation.Location.Name,
                LocationTypeId = reservation.Location.LocationTypeId,
                lstReservation_Details = reservationTransaction.ReservationDetails.ToList(),
                LocationTypeName = reservation.Location.LocationType.Name,
                Platform = "Mobile"
            };

            return new Response<WorkSpaceReservationHistoryResponse>(reservationInfo);
        }

        private async Task<Response<WorkSpaceReservationHistoryResponse>> GetReservationInfoTailored(GetWorkSpaceReservationHistoryDto request)
        {
            var reservation = await _unitOfWork.WorkSpaceReservationTailoredRepo
                                               .GetReservationInfo(request.WorkSpaceReservationId);


            if (reservation == null)
            {
                return new Response<WorkSpaceReservationHistoryResponse>("there's no such Reservation");
            }

            var reservationTransaction = await _unitOfWork.WorkSpaceReservationTailoredRepo
                                          .GetRelatedReservationTransaction(request.WorkSpaceReservationId,
                                                                            request.ReservationTypeId);

            // Get Entry Scan Time 

            var entryScanTime = reservationTransaction.ReservationDetails
                                                      .OrderByDescending(r => r.CreatedAt)
                                                      .FirstOrDefault().StartDateTime;

            // Get Reservation Status
            var status = GetReservationStatus(reservationTransaction);

            if (string.IsNullOrEmpty(status))
            {
                return new Response<WorkSpaceReservationHistoryResponse>("there's missing Reservation info");
            }

            // List of Foodics Details

            // Set top ups history, and Gifted Hours
            List<WorkSpaceTopupHistoryResponse> topupHistory = new List<WorkSpaceTopupHistoryResponse>();
            List<GiftedHours> giftedHours = new List<GiftedHours>();

            foreach (var item in reservation.TopUps)
            {
                if (item.Description == null)
                {
                    topupHistory.Add(new WorkSpaceTopupHistoryResponse
                    {
                        Id = item.Id,
                        Amount = item.TailoredHours,
                        CreatedAt = item.CreatedAt,
                        CreatedBy = item.CreatedBy,
                        Type = "Hourly",
                        Payment_Method = item.PaymentMethod.Name,
                        Hours = item.TailoredHours,
                    });
                }
                else
                {
                    giftedHours.Add(new GiftedHours
                    {
                        Reason = item.Description,
                        AdminName = item.CreatedBy,
                        CreatedAt = item.CreatedAt,
                        HoursAdded = item.TailoredHours, 
                    });
                }
            }

            var reservationInfo = new WorkSpaceReservationHistoryResponse
            {
                Id = reservation.Id,
                BasicUserId = reservation.BasicUserId,
                FirstName = reservation.BasicUser.FirstName,
                LastName = reservation.BasicUser.LastName,
                Amount = reservation.TailoredPrice,
                PaymentMethod = reservation.PaymentMethodId,
                Status = status,
                Mode = reservation.TopUps.Count == 0 ? "Basic" : "TopUp",
                ReservationType = "Tailored",
                ReservationTypeId = 1,
                EntryScanTime = entryScanTime,
                lstGiftedHours = giftedHours,
                lstTopupHistory = topupHistory,
                OpportunityStartDate = reservation.CreatedAt,
                EndDate = reservationTransaction.ExtendExpiryDate,
                CreditHours = reservationTransaction.RemainingHours,
                CountryCode = reservation.BasicUser.Country.CountryCode,
                MobileNumber = reservation.BasicUser.MobileNumber,
                DateTime = reservation.TailoredStartDate,
                LocationId = reservation.LocationId,
                LocationName = reservation.Location.Name,
                LocationTypeId = reservation.Location.LocationTypeId,
                lstReservation_Details = reservationTransaction.ReservationDetails.ToList(),
                LocationTypeName = reservation.Location.LocationType.Name,
                Platform = "Mobile"
            };

            return new Response<WorkSpaceReservationHistoryResponse>(reservationInfo);
        }

        private async Task<Response<WorkSpaceReservationHistoryResponse>> GetReservationInfoBundle(GetWorkSpaceReservationHistoryDto request)
        {
            var reservation = await _unitOfWork.WorkSpaceReservationBundleRepo
                                               .GetReservationInfo(request.WorkSpaceReservationId);

            if (reservation == null)
            {
                return new Response<WorkSpaceReservationHistoryResponse>("there's no such Reservation");
            }

            var reservationTransaction = await _unitOfWork.WorkSpaceReservationBundleRepo
                                                      .GetRelatedReservationTransaction(request.WorkSpaceReservationId,
                                                                                        request.ReservationTypeId);

            // Get Entry Scan Time 

            var entryScanTime = reservationTransaction.ReservationDetails
                                                      .OrderByDescending(r => r.CreatedAt)
                                                      .FirstOrDefault().StartDateTime;

            // Get Reservation Status
            var status = GetReservationStatus(reservationTransaction);

            if (string.IsNullOrEmpty(status))
            {
                return new Response<WorkSpaceReservationHistoryResponse>("there's missing Reservation info");
            }

            // List of Foodics Details


            var reservationInfo = new WorkSpaceReservationHistoryResponse
            {
                Id = reservation.Id,
                BasicUserId = reservation.BasicUserId,
                FirstName = reservation.BasicUser.FirstName,
                LastName = reservation.BasicUser.LastName,
                Amount = reservation.PackagePrice,
                PaymentMethod = reservation.PaymentMethodId,
                Status = status,
                Mode = "Basic",
                ReservationType = "Bundle",
                ReservationTypeId = 1,
                EntryScanTime = entryScanTime,
                OpportunityStartDate = reservation.CreatedAt,
                EndDate = reservationTransaction.ExtendExpiryDate,
                CreditHours = reservationTransaction.RemainingHours,
                CountryCode = reservation.BasicUser.Country.CountryCode,
                MobileNumber = reservation.BasicUser.MobileNumber,
                DateTime = reservation.PackageStartDate,
                LocationId = reservation.LocationId,
                LocationName = reservation.Location.Name,
                LocationTypeId = reservation.Location.LocationTypeId,
                lstReservation_Details = reservationTransaction.ReservationDetails.ToList(),
                LocationTypeName = reservation.Location.LocationType.Name,
                Platform = "Mobile"
            };

            return new Response<WorkSpaceReservationHistoryResponse>(reservationInfo);

        }

        private string GetReservationStatus(ReservationTransaction reservationTransaction)
        {
            string status = string.Empty;

            var expiryDate = reservationTransaction.ExtendExpiryDate ?? null;

            if (expiryDate is null)
            {
                return status;
            }

            var isExpired = DateTime.Compare(_dateTimeService.NowUtc, expiryDate.Value);

            if (isExpired > 0 || isExpired == 0)
                status = "Closed";

            else
            {
                var isScannedIn = reservationTransaction.ReservationDetails.Count > 0;

                status = isScannedIn ? "Open" : "New";
            }

            return status;
        }
    }
}
