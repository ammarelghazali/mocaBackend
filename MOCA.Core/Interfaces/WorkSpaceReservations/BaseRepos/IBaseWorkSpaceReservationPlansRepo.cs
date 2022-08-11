using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.BaseRepos
{
    public interface IBaseWorkSpaceReservationPlansRepo<T> where T : BaseEntity
    {
        Task<T> GetReservationInfo(long id);
        IQueryable<GetAllWorkSpaceReservationsResponse> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request);
    }
}
