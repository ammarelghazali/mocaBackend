using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.Interfaces.WorkSpaceReservations.BaseServices;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.WorkSpaces.Services
{
    public interface IWorkSpaceReservationServiceTailored : IBaseWorkSpaceReservationPlansService
    {
        Task<Response<SharedCreationResponse>> CreateTopUp(CreateWorkSpaceTopUp topUp);
    }
}
