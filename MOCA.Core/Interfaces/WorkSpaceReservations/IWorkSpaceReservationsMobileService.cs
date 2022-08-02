using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation.Mobile.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.Mobile.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Interfaces.WorkSpaceReservations
{
    public interface IWorkSpaceReservationsMobileService
    {
        Task<Response<WorkspaceReservationHomePageResponse>> GetWorkSpaceReservationHomePage(WorkspaceReservationHomePageDto request);
    }
}
