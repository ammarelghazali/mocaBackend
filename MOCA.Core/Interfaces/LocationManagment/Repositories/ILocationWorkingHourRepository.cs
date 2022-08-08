using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface ILocationWorkingHourRepository : IGenericRepository<LocationWorkingHour>
    {
        Task<bool> DeleteAllLocationWorkingHourByLocationID(long LocationID);
        Task<List<LocationWorkingHour>> GetAllLocationWorkingHourByLocationID(long LocationID);
    }
}
