using MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces;
using MOCA.Core.Interfaces.Base;
using MOCA.Core.Interfaces.WorkSpaceReservations.BaseRepos;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.WorkSpaces.Repositories
{
    public interface IWorkSpaceReservationTailoredRepo : IBaseWorkSpaceReservationPlansRepo<WorkSpaceReservationTailored>, IGenericRepository<WorkSpaceReservationTailored>
    {
        Task<WorkSpaceReservationTailored> GetReservationById(long id);
    }
}
