using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class LocationInclusionRepository : GenericRepository<LocationInclusion>, ILocationInclusionRepository
    {
        private readonly ApplicationDbContext _context;
        public LocationInclusionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAllLocationInclusionByLocationID(long LocationID)
        {
            var LocationInclusions = _context.LocationInclusions.Where(x => x.LocationId == LocationID && x.IsDeleted != true).ToList();

            _context.LocationInclusions.RemoveRange(LocationInclusions);
            return true;
        }

        public async Task<List<LocationInclusion>> GetAllLocationInclusionByLocationID(long LocationID)
        {
            var LocationInclusions = _context.LocationInclusions.Where(x => x.LocationId == LocationID && x.IsDeleted != true).ToList();
            return new List<LocationInclusion>(LocationInclusions);
        }
    }
}
