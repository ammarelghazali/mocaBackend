using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly IWorkSpaceReservationServiceHourly _hourlyService;
        private readonly IWorkSpaceReservationServiceBundle _bundleService;
        private readonly IWorkSpaceReservationServiceTailored _tailoredService;

        public WorkSpaceReservationsServiceCRM(IUnitOfWork unitOfWork, IMapper mapper, IDateTimeService dateTimeService, 
                                               IWorkSpaceReservationServiceHourly hourlyService,
                                               IWorkSpaceReservationServiceTailored tailoredService,
                                               IWorkSpaceReservationServiceBundle bundleService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
            _hourlyService = hourlyService;
            _bundleService = bundleService;
            _tailoredService = tailoredService;
        }

        public IWorkSpaceReservationServiceTailored TailoredService { get; }

        public async Task<PagedResponse<IReadOnlyList<GetAllWorkSpaceReservationsResponse>>> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request)
        {
            // get pg_total

            var hourlyReservations = await _unitOfWork.WorkSpaceReservationHourlyRepo.GetAllWorkSpaceSubmissions(request);
            var tailoredReservations = await _unitOfWork.WorkSpaceReservationTailoredRepo.GetAllWorkSpaceSubmissions(request);
            var bundleReservations = await _unitOfWork.WorkSpaceReservationBundleRepo.GetAllWorkSpaceSubmissions(request);

            var allReservations = await hourlyReservations.Union(tailoredReservations)
                                                          .Union(bundleReservations)
                                                          .OrderByDescending(r => r.OpportunityStartDate)
                                                          .ToListAsync();


            var paginatedReservation = allReservations.Skip(request.pageSize * (request.pageNumber - 1))
                                                      .Take(request.pageSize).ToList();

            foreach (var item in paginatedReservation)
            {
                // get cart currency

                var reservationTransaction = await _unitOfWork.WorkSpaceReservationHourlyRepo
                                                     .GetRelatedReservationTransaction(item.Id, item.ReservationTypeId);

                item.CreditHours = reservationTransaction.RemainingHours;
                item.EndDate = reservationTransaction.ExtendExpiryDate;

                item.EntryScanTime = reservationTransaction.ReservationDetails
                                                      .OrderByDescending(r => r.CreatedAt)
                                                      .FirstOrDefault().StartDateTime;

                if (string.IsNullOrEmpty(item.TopUpsLink))
                {
                    item.TopUpsLink = item.Mode == "TopUp" ? "resources/templates/check.png" : "resources/templates/unchecked.png";
                }

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

            if (paginatedReservation.Count > 0)
            {
                return new PagedResponse<IReadOnlyList<GetAllWorkSpaceReservationsResponse>>(paginatedReservation, 
                                                                                             request.pageNumber, 
                                                                                             request.pageSize, 
                                                                                             (int)Math.Ceiling((double)allReservations.Count / request.pageSize) );
            }
            return new PagedResponse<IReadOnlyList<GetAllWorkSpaceReservationsResponse>>(null, request.pageNumber, request.pageSize);
        }

        public async Task<PagedResponse<IReadOnlyList<GetFilteredWorkSpaceReservationResponse>>> GetFilteredSubmissions(GetFilteredWorkSpaceReservationDto request)
        {
            var data = await _unitOfWork.WorkSpaceReservationsRepositoryCRM.GetFilteredSubmissions(request);

            if (data.Count > 0)
            {
                return new PagedResponse<IReadOnlyList<GetFilteredWorkSpaceReservationResponse>>(data, request.pageNumber, request.pageSize, data.Count);

            }
            return new PagedResponse<IReadOnlyList<GetFilteredWorkSpaceReservationResponse>>(null, request.pageNumber, request.pageSize);
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
            if (request.ReservationTypeId == 1)
            {
                return await _hourlyService.GetReservationInfo(request);
            }
            else if(request.ReservationTypeId == 2)
            {
                return await _tailoredService.GetReservationInfo(request);
            }
            else if(request.ReservationTypeId == 3)
            {
                return await _bundleService.GetReservationInfo(request);
            }

            return new Response<WorkSpaceReservationHistoryResponse>("ReservationTypeId is not correct");
        }

    }
}
