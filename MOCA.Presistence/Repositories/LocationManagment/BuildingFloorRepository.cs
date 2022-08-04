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

    }
}
