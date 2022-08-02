using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.DTOs.WorkSpaceReservation.Mobile.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.Services
{
    public interface IWorkSpaceReservationsServiceCRM
    {
        Task<PagedResponse<IReadOnlyList<GetAllWorkSpaceSubmissionsResponse>>> GetAllWorkSpaceSubmissions(GetAllWorkSpaceSubmissionsDto request);
    }
}
