using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.WorkSpaces.Services
{
    public interface IWorkSpaceReservationsServiceCRM
    {
        Task<PagedResponse<IReadOnlyList<GetAllWorkSpaceReservationsResponse>>> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request);
        Task<Response<WorkSpaceReservationHistoryResponse>> GetWorkSpaceOpportunityInfoHistory(GetWorkSpaceReservationHistoryDto request);
        Task<Response<WorkSpaceReservationLocationsDropDown>> GetWorkSpaceLocationsDropDowns();
        Task<PagedResponse<IReadOnlyList<GetFilteredWorkSpaceReservationResponse>>> GetFilteredSubmissions(GetFilteredWorkSpaceReservationDto request);
        Task<Response<IReadOnlyList<GetFilteredWorkSpaceReservationNotPaginatedResponse>>> GetFilteredSubmissionsWithoutPagination(GetAllWorkSpaceReservationNotPaginated request);
        Task<Response<SharedCreationResponse>> CreateTopUp(CreateWorkSpaceTopUp topUp);
    }
}
