using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface IBuildingFloorRepository : IGenericRepository<BuildingFloor>
    {
        Task<bool> CheckBuildingFloorExistence(long BuildingId, long FloorNumber);
        Task<bool> DeleteBuildingFloorByBuildingId(long BuildingId);
        Task<List<BuildingFloor>> GetAllBuildingFloorByBuildingId(long BuildingId);
        Task<int> CountBuildingFloorByBuildingId(long BuildingId);
    }
}
