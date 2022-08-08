using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface ILocationImageRepository : IGenericRepository<LocationImage>
    {
        Task<bool> DeleteAllLocationImageByLocationID(long LocationID);
        Task<List<LocationImage>> GetAllLocationImageByLocationID(long LocationID);
    }
}
