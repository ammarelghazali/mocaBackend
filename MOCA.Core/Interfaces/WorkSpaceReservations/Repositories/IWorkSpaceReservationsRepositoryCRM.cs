using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Entities.WorkSpaceReservations;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.Repositories
{
    public interface IWorkSpaceReservationsRepositoryCRM 
    {
        Task<IReadOnlyList<GetFilteredWorkSpaceReservationResponse>> GetFilteredSubmissions(GetFilteredWorkSpaceReservationDto request);
        Task<List<DropdownViewModel>> GetWorkSpaceLocationsDropDowns();
        Task<IReadOnlyList<GetFilteredWorkSpaceReservationNotPaginatedResponse>> GetFilteredSubmissionsWithoutPagination(GetAllWorkSpaceReservationNotPaginated request);
    }
}
