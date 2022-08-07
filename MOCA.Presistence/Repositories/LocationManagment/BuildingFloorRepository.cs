using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class BuildingFloorRepository : GenericRepository<BuildingFloor>, IBuildingFloorRepository
    {
        private readonly ApplicationDbContext _context;
        public BuildingFloorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckBuildingFloorExistence(long BuildingId, long FloorNumber)
        {
            if (BuildingId <= 0)
                return false;

            var BuildingFloor = _context.BuildingFloors.Where(x => x.BuildingId == BuildingId && x.FloorNumber == FloorNumber && x.IsDeleted != true).FirstOrDefault();
            if (BuildingFloor == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteBuildingFloorByBuildingId(long BuildingId)
        {
            var buildingFloor = _context.BuildingFloors.Where(x => x.BuildingId == BuildingId && x.IsDeleted != true).ToList();
            if (buildingFloor.Count != 0)
            {
                _context.BuildingFloors.RemoveRange(buildingFloor);
            }
            
            return true;
        }

        public async Task<List<BuildingFloor>> GetAllBuildingFloorByBuildingId(long BuildingId)
        {
            if (BuildingId <= 0)
                return new List<BuildingFloor>();

            var BuildingFloor = _context.BuildingFloors.Where(x => x.BuildingId == BuildingId && x.IsDeleted != true).ToList();
            return new List<BuildingFloor>(BuildingFloor);
        }

        public async Task<int> CountBuildingFloorByBuildingId(long BuildingId)
        {
            var countBuildingFloor = _context.BuildingFloors.Where(x => x.BuildingId == BuildingId && x.IsDeleted != true).Count();
            return countBuildingFloor;
        }
    }
}
