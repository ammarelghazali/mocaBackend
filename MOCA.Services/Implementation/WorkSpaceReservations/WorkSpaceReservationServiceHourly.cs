using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Interfaces.WorkSpaceReservations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Services.Implementation.WorkSpaceReservations
{
    public class WorkSpaceReservationServiceHourly : IWorkSpaceReservationServiceHourly
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;

        public WorkSpaceReservationServiceHourly(IUnitOfWork unitOfWork, IMapper mapper, IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
        }

        public async Task<List<GetAllWorkSpaceReservationsResponse>> GetAllWorkSpaceReservations(GetAllWorkSpaceReservationsDto request)
        {
            var hourlyReservations = await _unitOfWork.WorkSpaceReservationHourlyRepo.GetAllWorkSpaceSubmissions(request);

            foreach (var item in hourlyReservations)
            {
                // get cart currency

                var reservationTransaction = await _unitOfWork.WorkSpaceReservationHourlyRepo
                                                     .GetRelatedReservationTransaction(item.Id, 1);

                item.CreditHours = reservationTransaction.RemainingHours;
                item.EndDate = reservationTransaction.ExtendExpiryDate;
                item.EntryScanTime = reservationTransaction.ReservationDetails
                                                      .OrderByDescending(r => r.CreatedAt)
                                                      .FirstOrDefault().StartDateTime;

                item.TopUpsLink = item.Mode == "TopUp" ? "resources/templates/check.png" : "resources/templates/unchecked.png";
                item.Scanin = reservationTransaction.ReservationDetails.Select(r => r.StartDateTime).FirstOrDefault();
                item.ScanOut = reservationTransaction.ReservationDetails.OrderByDescending(r => r.Id)
                                                                        .Select(r => r.EndDateTime).FirstOrDefault();

                string status = string.Empty;

                var expiryDate = reservationTransaction.ExtendExpiryDate ?? null;

                if (expiryDate is not null)
                {
                    var isExpired = DateTime.Compare(_dateTimeService.NowUtc, expiryDate.Value);

                    if (isExpired > 0 || isExpired == 0)
                        status = "Closed";

                    else
                    {
                        var isScannedIn = reservationTransaction.ReservationDetails.Count > 0;

                        status = isScannedIn ? "Open" : "New";
                    }
                }

                item.Status = status;
            }


            return hourlyReservations;
        }

        public async Task<Response<WorkSpaceReservationHistoryResponse>> GetReservationInfo(GetWorkSpaceReservationHistoryDto request)
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
            string status;

            var expiryDate = reservationTransaction.ExtendExpiryDate ?? null;

            if (expiryDate is null)
            {
                return new Response<WorkSpaceReservationHistoryResponse>("there's missing Reservation info");
            }

            var isExpired = DateTime.Compare(_dateTimeService.NowUtc, expiryDate.Value);

            if (isExpired > 0 || isExpired == 0)
                status = "Closed";

            else
            {
                var isScannedIn = reservationTransaction.ReservationDetails.Count > 0;

                status = isScannedIn ? "Open" : "New";
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
    }
}
