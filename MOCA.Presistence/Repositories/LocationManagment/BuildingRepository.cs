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
    }
}
