using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MOCA.Core;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Interfaces.WorkSpaceReservations.WorkSpaces.Services;

namespace MOCA.Services.Implementation.WorkSpaceReservations.WorkSpaces
{
    public class WorkSpaceReservationServiceBundle : IWorkSpaceReservationServiceBundle
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IReservationsStatusService _reservationsStatusService;

        public WorkSpaceReservationServiceBundle(IUnitOfWork unitOfWork, IMapper mapper, IDateTimeService dateTimeService,
                                                 IReservationsStatusService reservationsStatusService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
            _reservationsStatusService = reservationsStatusService;
        }

        public async Task<List<GetAllWorkSpaceReservationsResponse>> GetAllWorkSpaceReservations(GetAllWorkSpaceReservationsDto request)
        {
            var bundleReservations = _unitOfWork.WorkSpaceReservationBundleRepo.GetAllWorkSpaceSubmissions(request);

            return await bundleReservations.Skip(request.pageSize * (request.pageNumber - 1)).Take(request.pageSize).ToListAsync();
        }

        public async Task<Response<WorkSpaceReservationHistoryResponse>> GetReservationInfo(GetWorkSpaceReservationHistoryDto request)
        {
            var reservation = await _unitOfWork.WorkSpaceReservationBundleRepo
                                                          .GetReservationInfo(request.WorkSpaceReservationId);

            if (reservation == null)
            {
                return new Response<WorkSpaceReservationHistoryResponse>("there's no such Reservation");
            }

            // Get Entry Scan Time 

            var entryScanTime = reservation.WorkSpaceBundleTransactions.ReservationTransaction.ReservationDetails
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
                Status = _reservationsStatusService.GetStatus(reservation.WorkSpaceBundleTransactions.ReservationTransaction,
                                                              reservation.WorkSpaceBundleCancellation.CancelReservation),
                Mode = "Basic",
                ReservationType = "Bundle",
                ReservationTypeId = 1,
                EntryScanTime = entryScanTime,
                OpportunityStartDate = reservation.CreatedAt,
                EndDate = reservation.WorkSpaceBundleTransactions.ReservationTransaction.ExtendExpiryDate,
                CreditHours = reservation.WorkSpaceBundleTransactions.ReservationTransaction.RemainingHours,
                CountryCode = reservation.BasicUser.Country.CountryCode,
                MobileNumber = reservation.BasicUser.MobileNumber,
                DateTime = reservation.BundleStartDate,
                LocationId = reservation.LocationId,
                LocationName = reservation.Location.Name,
                LocationTypeId = reservation.Location.LocationTypeId,
                ReservationDetails = reservation.WorkSpaceBundleTransactions.ReservationTransaction.ReservationDetails.ToList(),
                LocationTypeName = reservation.Location.LocationType.Name,
                Platform = "Mobile"
            };

            return new Response<WorkSpaceReservationHistoryResponse>(reservationInfo);
        }
    }
}
