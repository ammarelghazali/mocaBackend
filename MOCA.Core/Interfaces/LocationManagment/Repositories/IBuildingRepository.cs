using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface IBuildingRepository : IGenericRepository<Building>
    {
        Task<bool> CheckBuildingExistence(long LocationID, string BuildingName);
    }
}
