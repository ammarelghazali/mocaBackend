using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.WorkSpaces.Services
{
    public interface IWorkSpaceReservationServiceTailored
    {
        Task<Response<WorkSpaceReservationHistoryResponse>> GetReservationInfo(GetWorkSpaceReservationHistoryDto request);
        Task<List<GetAllWorkSpaceReservationsResponse>> GetAllWorkSpaceReservations(GetAllWorkSpaceReservationsDto request);
        Task<Response<SharedCreationResponse>> CreateTopUp(CreateWorkSpaceTopUp topUp);
    }
}
