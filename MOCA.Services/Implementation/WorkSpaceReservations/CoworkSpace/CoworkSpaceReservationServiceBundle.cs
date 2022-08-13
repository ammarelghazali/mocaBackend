using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MOCA.Core;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Interfaces.WorkSpaceReservations.CoworkSpace.Services;

namespace MOCA.Services.Implementation.WorkSpaceReservations.CoworkSpace
{
    public class CoworkSpaceReservationServiceBundle : ICoworkSpaceReservationServiceBundle
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IReservationsStatusService _reservationsStatusService;

        public CoworkSpaceReservationServiceBundle(IUnitOfWork unitOfWork, IMapper mapper, IDateTimeService dateTimeService,
                                                 IReservationsStatusService reservationsStatusService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
            _reservationsStatusService = reservationsStatusService;
        }

        public async Task<List<GetAllWorkSpaceReservationsResponse>> GetAllWorkSpaceReservations(GetAllWorkSpaceReservationsDto request)
        {
            var bundleReservations = _unitOfWork.CoworkSpaceReservationBundleRepo.GetAllWorkSpaceSubmissions(request);

            return await bundleReservations.Skip(request.pageSize * (request.pageNumber - 1)).Take(request.pageSize).ToListAsync();
        }

        public async Task<Response<WorkSpaceReservationHistoryResponse>> GetReservationInfo(GetWorkSpaceReservationHistoryDto request)
        {
            var reservation = await _unitOfWork.CoworkSpaceReservationBundleRepo
                                                                      .GetReservationInfo(request.WorkSpaceReservationId);

            if (reservation == null)
            {
                return new Response<WorkSpaceReservationHistoryResponse>("there's no such Reservation");
            }

            // Get Entry Scan Time 

            var entryScanTime = reservation.CoworkingSpaceBundleTransaction.ReservationTransaction.ReservationDetails
                                                      .OrderByDescending(r => r.CreatedAt)
                                                      .FirstOrDefault().StartDateTime;


            // List of Foodics Details


            var reservationInfo = new WorkSpaceReservationHistoryResponse
            {
                Id = reservation.Id,
                BasicUserId = reservation.BasicUserId,
                FirstName = reservation.BasicUser.FirstName,
                LastName = reservation.BasicUser.LastName,
                Amount = reservation.BundlePrice,
                PaymentMethod = reservation.PaymentMethodId,
                Status = _reservationsStatusService.GetStatus(reservation.CoworkingSpaceBundleTransaction.ReservationTransaction,
                                                              reservation.CoworkingSpaceBundleCancellation.CancelReservation),
                Mode = "Basic",
                ReservationType = "Bundle",
                ReservationTypeId = 1,
                EntryScanTime = entryScanTime,
                OpportunityStartDate = reservation.CreatedAt,
                EndDate = reservation.CoworkingSpaceBundleTransaction.ReservationTransaction.ExtendExpiryDate,
                CreditHours = reservation.CoworkingSpaceBundleTransaction.ReservationTransaction.RemainingHours,
                CountryCode = reservation.BasicUser.Country.CountryCode,
                MobileNumber = reservation.BasicUser.MobileNumber,
                DateTime = reservation.BundleStartDate,
                LocationId = reservation.LocationId,
                LocationName = reservation.Location.Name,
                LocationTypeId = reservation.Location.LocationTypeId,
                ReservationDetails = reservation.CoworkingSpaceBundleTransaction.ReservationTransaction.ReservationDetails.ToList(),
                LocationTypeName = reservation.Location.LocationType.Name,
                Platform = "Mobile"
            };

            return new Response<WorkSpaceReservationHistoryResponse>(reservationInfo);
        }
    }
}
