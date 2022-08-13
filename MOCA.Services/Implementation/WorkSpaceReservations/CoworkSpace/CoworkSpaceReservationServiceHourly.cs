using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MOCA.Core;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Interfaces.WorkSpaceReservations.CoworkSpace.Services;

namespace MOCA.Services.Implementation.WorkSpaceReservations.CoworkSpace
{
    public class CoworkSpaceReservationServiceHourly : ICoworkSpaceReservationServiceHourly
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IReservationsStatusService _reservationsStatusService;

        public CoworkSpaceReservationServiceHourly(IUnitOfWork unitOfWork, IMapper mapper, IDateTimeService dateTimeService,
                                                 IReservationsStatusService reservationsStatusService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
            _reservationsStatusService = reservationsStatusService;
        }

        public Task<Response<SharedCreationResponse>> CreateTopUp(CreateWorkSpaceTopUp topUp)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetAllWorkSpaceReservationsResponse>> GetAllWorkSpaceReservations(GetAllWorkSpaceReservationsDto request)
        {
            var hourlyReservations = _unitOfWork.CoworkSpaceReservationHourlyRepo.GetAllWorkSpaceSubmissions(request);

            return await hourlyReservations.Skip(request.pageSize * (request.pageNumber - 1)).Take(request.pageSize).ToListAsync();
        }

        public async Task<Response<WorkSpaceReservationHistoryResponse>> GetReservationInfo(GetWorkSpaceReservationHistoryDto request)
        {
            var reservation = await _unitOfWork.CoworkSpaceReservationHourlyRepo
                                                        .GetReservationInfo(request.WorkSpaceReservationId);

            if (reservation == null)
            {
                return new Response<WorkSpaceReservationHistoryResponse>("there's no such Reservation");
            }

            // Get Entry Scan Time 

            var entryScanTime = reservation.CoworkingSpaceHourlyTransaction.ReservationTransaction.ReservationDetails
                                                      .OrderByDescending(r => r.CreatedAt)
                                                      .FirstOrDefault().StartDateTime;


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
                Status = _reservationsStatusService.GetStatus(reservation.CoworkingSpaceHourlyTransaction.ReservationTransaction,
                                                              reservation.CoworkingSpaceHourlyCancellation.CancelReservation),
                Mode = reservation.TopUps.Count == 0 ? "Basic" : "TopUp",
                ReservationType = "Hourly",
                ReservationTypeId = 1,
                EntryScanTime = entryScanTime,
                GiftedHours = giftedHours,
                TopupHistory = topupHistory,
                OpportunityStartDate = reservation.CreatedAt,
                EndDate = reservation.CoworkingSpaceHourlyTransaction.ReservationTransaction.ExtendExpiryDate,
                CreditHours = reservation.CoworkingSpaceHourlyTransaction.ReservationTransaction.RemainingHours,
                CountryCode = reservation.BasicUser.Country.CountryCode,
                MobileNumber = reservation.BasicUser.MobileNumber,
                DateTime = reservation.Date,
                LocationId = reservation.LocationId,
                LocationName = reservation.Location.Name,
                LocationTypeId = reservation.Location.LocationTypeId,
                ReservationDetails = reservation.CoworkingSpaceHourlyTransaction.ReservationTransaction.ReservationDetails.ToList(),
                LocationTypeName = reservation.Location.LocationType.Name,
                Platform = "Mobile"
            };

            return new Response<WorkSpaceReservationHistoryResponse>(reservationInfo);
        }
    }
}
