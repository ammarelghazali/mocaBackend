using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface ILocationContactRepository : IGenericRepository<LocationContact>
    {
        Task<bool> DeleteAllLocationContactByLocationID(long LocationID);
        Task<List<LocationContact>> GetAllLocationContactByLocationID(long LocationID);
    }
}
