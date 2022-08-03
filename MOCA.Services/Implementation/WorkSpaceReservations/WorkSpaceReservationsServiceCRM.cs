using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Interfaces.WorkSpaceReservations.Services;

namespace MOCA.Services.Implementation.WorkSpaceReservations
{
    public class WorkSpaceReservationsServiceCRM : IWorkSpaceReservationsServiceCRM
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkSpaceReservationsServiceCRM(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        public Task<Response<WorkSpaceReservationHistoryResponse>> GetWorkSpaceOpportunityInfoHistory(GetWorkSpaceReservationHistoryDto request)
        {
            throw new NotImplementedException();

            dynamic reservation = null;

            if(request.ReservationTypeId == 1)
            {

            }
            else if(request.ReservationTypeId == 2)
            {

            }
            else if(request.ReservationTypeId == 3)
            {

            }


            // From Id, and ReservationTypeId, Choose the right WorkSpaceReservation Tables depend on type id to get where same id
            // with include 1. BasicUser, 2. Location .. then Include LocationType,
            // 3. ReservationTransactions..then Include Reservation Details
            // 4. ReservationType, 5. Top Ups

            // Get Entry Scan Time 
            // Get Reservation Status

            // List of Foodics Details

            // Set top ups history, and Gifted Hours
        }
    }
}
