using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class IndustryRepository : GenericRepository<Industry>, IIndustryRepository
    {
        private readonly ApplicationDbContext _context;
        public IndustryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteIndustry(long Id)
        {
            var industry = _context.Industries.Where(x => x.Id == Id && x.IsDeleted != true).FirstOrDefault();
            if (industry == null)
            {
                return false;
            }
            _context.Industries.Remove(industry);
            return true;
        }

        public async Task<bool> HasAnyRelatedEntities(long IndustryID)
        {
            if (IndustryID <= 0) return false;
            return await _context.LocationIndustries.AnyAsync(x => x.IndustryId.Equals(IndustryID));
        }
    }
}
