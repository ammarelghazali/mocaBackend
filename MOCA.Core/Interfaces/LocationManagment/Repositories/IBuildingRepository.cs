using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface IBuildingRepository : IGenericRepository<Building>
    {
        Task<bool> CheckBuildingExistence(long LocationID, string BuildingName);
        Task<bool> DeleteBuilding(long Id);
        Task<List<Building>> GetAllBuildingByLocationId(long LocationId);
    }
}
