using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class BuildingRepository : GenericRepository<Building>, IBuildingRepository
    {
        private readonly ApplicationDbContext _context;
        public BuildingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckBuildingExistence(long LocationID, string BuildingName)
        {
            if (LocationID <= 0)
                return false;

            var Building = _context.Buildings.Where(x => x.LocationId == LocationID && x.Name == BuildingName && x.IsDeleted != true).FirstOrDefault();
            if (Building == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteBuilding(long Id)
        {
            var building = _context.Buildings.Where(x => x.Id == Id && x.IsDeleted != true).FirstOrDefault();
            if (building == null)
            {
                return false;
            }
            _context.Buildings.Remove(building);
            return true;
        }

        public async Task<List<Building>> GetAllBuildingByLocationId(long LocationId)
        {
            var Building = _context.Buildings.Where(x => x.LocationId == LocationId && x.IsDeleted != true).ToList();
            if (Building == null)
            {
                return new List<Building>();
            }

            return new List<Building>(Building);
        }
    }
}
