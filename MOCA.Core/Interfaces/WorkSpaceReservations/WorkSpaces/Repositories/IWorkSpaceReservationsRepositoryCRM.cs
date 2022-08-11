using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.WorkSpaces.Repositories
{
    public interface IWorkSpaceReservationsRepositoryCRM
    {
        Task<IReadOnlyList<GetFilteredWorkSpaceReservationResponse>> GetFilteredSubmissions(GetFilteredWorkSpaceReservationDto request);
        Task<List<DropdownViewModel>> GetWorkSpaceLocationsDropDowns();
        Task<IReadOnlyList<GetFilteredWorkSpaceReservationNotPaginatedResponse>> GetFilteredSubmissionsWithoutPagination(GetAllWorkSpaceReservationNotPaginated request);
    }
}
