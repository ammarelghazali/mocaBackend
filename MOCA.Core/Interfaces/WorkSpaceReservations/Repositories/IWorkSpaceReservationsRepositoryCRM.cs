using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Entities.WorkSpaceReservations;
using MOCA.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.Repositories
{
    public interface IWorkSpaceReservationsRepositoryCRM 
    {
        Task<IReadOnlyList<GetAllWorkSpaceReservationsResponse>> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request);
        Task<IReadOnlyList<GatFilteredWorkSpaceReservationResponse>> GetFilteredSubmissions(GatFilteredWorkSpaceReservationDto request);
    }
}
