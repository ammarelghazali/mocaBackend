using MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces;
using MOCA.Core.Interfaces.Base;
using MOCA.Core.Interfaces.WorkSpaceReservations.BaseRepos;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.WorkSpaces.Repositories
{
    public interface IWorkSpaceReservationHourlyRepo : IBaseWorkSpaceReservationPlansRepo<WorkSpaceReservationHourly>, IGenericRepository<WorkSpaceReservationHourly>
    {
        Task<WorkSpaceReservationHourly> GetReservationById(long id);
    }
}
