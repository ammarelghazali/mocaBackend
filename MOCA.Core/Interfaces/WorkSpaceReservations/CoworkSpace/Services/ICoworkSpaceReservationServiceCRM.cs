using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Interfaces.WorkSpaceReservations.BaseServices;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.CoworkSpace.Services
{
    public interface ICoworkSpaceReservationServiceCRM : IBaseWorkSpaceReservationServiceCRM
    {
        Task<PagedResponse<IReadOnlyList<GetAllWorkSpaceReservationsResponse>>> GetAllWorkSpaceSubmissionsSP(GetAllWorkSpaceReservationsDto request);
    }
}
