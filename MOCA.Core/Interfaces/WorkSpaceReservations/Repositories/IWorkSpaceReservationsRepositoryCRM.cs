using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Entities.WorkSpaceReservations;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.Repositories
{
    public interface IWorkSpaceReservationsRepositoryCRM 
    {
        Task<IReadOnlyList<GetAllWorkSpaceReservationsResponse>> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request);
        Task<IReadOnlyList<GatFilteredWorkSpaceReservationResponse>> GetFilteredSubmissions(GatFilteredWorkSpaceReservationDto request);
        Task<List<DropdownViewModel>> GetWorkSpaceLocationsDropDowns();
        //Task<WorkSpaceReservationHourly> GetHourlyReservationInfo(long id);
        //Task<WorkSpaceReservationTailored> GetTailoredReservationInfo(long id);
        //Task<WorkSpaceReservationBundle> GetBundleReservationInfo(long id);
    }
}
