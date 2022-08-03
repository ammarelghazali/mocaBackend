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

            var hourlyReservations = await _hourlyService.GetAllWorkSpaceReservations(request);
            var tailoredReservations = await _tailoredService.GetAllWorkSpaceReservations(request);
            var bundleReservations = await _bundleService.GetAllWorkSpaceReservations(request);

            var allSubmissions = new List<GetAllWorkSpaceReservationsResponse>();

            allSubmissions.AddRange(hourlyReservations);
            allSubmissions.AddRange(tailoredReservations);
            allSubmissions.AddRange(bundleReservations);

            allSubmissions.OrderByDescending(r => r.OpportunityStartDate);

            if (allSubmissions.Count > 0)
            {
                return new PagedResponse<IReadOnlyList<GetAllWorkSpaceReservationsResponse>>(allSubmissions, request.pageNumber, request.pageSize, allSubmissions.Count);
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
