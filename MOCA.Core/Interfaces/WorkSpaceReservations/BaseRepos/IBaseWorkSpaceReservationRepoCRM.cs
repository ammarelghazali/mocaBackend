using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.BaseRepos
{
    public interface IBaseWorkSpaceReservationRepoCRM
    {
        Task<List<DropdownViewModel>> GetWorkSpaceLocationsDropDowns();
    }
}
