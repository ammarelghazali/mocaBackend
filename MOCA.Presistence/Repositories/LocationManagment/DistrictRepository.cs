
using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

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

        public async Task<bool> IsUniqueNameAsync(string districtName, long? id = null)
        {
            if (string.IsNullOrEmpty(districtName)) return false;
            return await _context.Districts.Where(p => p.Id != id).AllAsync(p => p.DistrictName.Trim().ToLower() != districtName.Trim().ToLower());
        }

        public async Task<bool> IsUniqueNameAsync(string districtName)
        {
            if (string.IsNullOrEmpty(districtName)) return false;
            return await _context.Districts.Where(p => p.IsDeleted == false).AllAsync(p => p.DistrictName.Trim().ToLower() != districtName.Trim().ToLower());
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
