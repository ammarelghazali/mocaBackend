using MOCA.Core.Entities.WorkSpaceReservations.CoWorkSpace;
using MOCA.Core.Interfaces.Base;
using MOCA.Core.Interfaces.WorkSpaceReservations.BaseRepos;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.CoworkSpace.Repositories
{
    public interface ICoworkSpaceReservationHourlyRepo : IBaseWorkSpaceReservationPlansRepo<CoworkingSpaceReservationHourly>, IGenericRepository<CoworkingSpaceReservationHourly>
    {
        Task<CoworkingSpaceReservationHourly> GetReservationById(long id);
    }
}
