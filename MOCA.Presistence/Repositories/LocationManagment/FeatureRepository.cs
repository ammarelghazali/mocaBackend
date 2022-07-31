using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class FeatureRepository : GenericRepository<Feature>, IFeatureRepository
    {
        private readonly ApplicationDbContext _context;
        public FeatureRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteFeature(long Id)
        {
            var feature = _context.Features.Where(x => x.Id == Id && x.IsDeleted != true).FirstOrDefault();
            if (feature == null)
            {
                return false;
            }
            _context.Features.Remove(feature);
            return true;
        }

        public async Task<bool> HasAnyRelatedEntities(long FeatureID)
        {
            if (FeatureID <= 0) return false;
            return await _context.Locations.AnyAsync(x => x.DistrictId.Equals(FeatureID));
        }
    }
}
