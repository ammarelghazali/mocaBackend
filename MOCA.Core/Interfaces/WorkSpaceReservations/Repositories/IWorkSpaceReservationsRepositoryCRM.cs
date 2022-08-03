using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Entities.WorkSpaceReservations;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.Repositories
{
    public interface IWorkSpaceReservationsRepositoryCRM 
    {
        Task<IReadOnlyList<GatFilteredWorkSpaceReservationResponse>> GetFilteredSubmissions(GatFilteredWorkSpaceReservationDto request);
        Task<List<DropdownViewModel>> GetWorkSpaceLocationsDropDowns();
    }
}
