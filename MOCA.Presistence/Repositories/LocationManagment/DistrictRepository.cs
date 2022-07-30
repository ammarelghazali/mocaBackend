
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;
using System.Data.Entity;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class DistrictRepository : GenericRepository<District>, IDistrictRepository
    {
        private readonly ApplicationDbContext _context;
        public DistrictRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<District>> GetDistrictsByCityId(long cityId)
        {
            return await _context.Districts.Where(a => a.CityId == cityId && a.IsDeleted != true).AsNoTracking().ToListAsync();
        }

        public async Task<bool> HasAnyRelatedEntities(long DistrictId)
        {
            if (DistrictId <= 0) return false;
            return await _context.Locations.AnyAsync(x => x.DistrictId.Equals(DistrictId));
        }

        public async Task<bool> DeleteDistrict(long Id)
        {
            var district = _context.Districts.Where(x => x.Id == Id && x.IsDeleted != true).FirstOrDefault();
            if (district == null)
            {
                return false;
            }
            _context.Districts.Remove(district);
            return true;
        }
    }
}
