using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface IBuildingFloorRepository : IGenericRepository<BuildingFloor>
    {
        Task<bool> CheckBuildingFloorExistence(long BuildingId, long FloorNumber);
    }
}
