using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface ILocationTypeRepository : IGenericRepository<LocationType>
    {
        Task<bool> DeleteLocationType(long Id);
        Task<bool> CheckLocationTypeIsUinque(string LocationTypeName);
    }
}
