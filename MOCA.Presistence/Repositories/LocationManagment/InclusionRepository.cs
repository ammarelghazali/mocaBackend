using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class InclusionRepository : GenericRepository<Inclusion>, IInclusionRepository
    {
        private readonly ApplicationDbContext _context;
        public InclusionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteInclusion(long Id)
        {
            var inclusion = _context.Inclusions.Where(x => x.Id == Id && x.IsDeleted != true).FirstOrDefault();
            if (inclusion == null)
            {
                return false;
            }
            _context.Inclusions.Remove(inclusion);
            return true;
        }

        public async Task<bool> HasAnyRelatedEntities(long InclusionID)
        {
            if (InclusionID <= 0) return false;
            return await _context.LocationInclusions.AnyAsync(x => x.InclusionId.Equals(InclusionID));
        }
    }
}
