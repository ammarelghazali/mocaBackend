using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
        Task<bool> CheckLocationNameIsUinque(string LocationName);
        Task<bool> DeleteLocation(long Id);
        Task<List<long>> GetAllDistinictDistrict();
    }
}
