using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface ILocationFileRepository : IGenericRepository<LocationFile>
    {
        Task<bool> DeleteAllLocationFileByLocationID(long LocationID);
        Task<List<LocationFile>> GetAllLocationFileByLocationID(long LocationID);
    }
}
