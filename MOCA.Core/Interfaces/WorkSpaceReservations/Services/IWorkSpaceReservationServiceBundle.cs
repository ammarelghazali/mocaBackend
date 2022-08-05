using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.Services
{
    public interface IWorkSpaceReservationServiceBundle
    {
        Task<Response<WorkSpaceReservationHistoryResponse>> GetReservationInfo(GetWorkSpaceReservationHistoryDto request);
        Task<List<GetAllWorkSpaceReservationsResponse>> GetAllWorkSpaceReservations(GetAllWorkSpaceReservationsDto request);

    }
}
