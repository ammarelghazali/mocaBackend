using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation.Mobile.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.Mobile.Response;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.Services
{
    public interface IWorkSpaceReservationsMobileService
    {
        Task<Response<WorkspaceReservationHomePageResponse>> GetWorkSpaceReservationHomePage(WorkspaceReservationHomePageDto request);
    }
}
