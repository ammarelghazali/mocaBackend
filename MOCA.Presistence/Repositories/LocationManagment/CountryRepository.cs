using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly ApplicationDbContext _context;
        public CountryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsUniqueNameAsync(string countryName, long? id = null)
        {
            if (string.IsNullOrEmpty(countryName)) return false;
            return await _context.Countries.Where(p => p.Id != id).AllAsync(p => p.CountryName.Trim().ToLower() != countryName.Trim().ToLower());
        }

        public async Task<bool> HasAnyCities(long CountryID)
        {
            if (CountryID <= 0) return false;
            return await _context.Cities.AnyAsync(x => x.CountryId.Equals(CountryID));
        }

        public async Task<Country> CountryIsExists(long countryID)
        {
            if (countryID <= 0) { return null; }

            var res = await _context.Countries.FirstOrDefaultAsync(x => x.Id.Equals(countryID));

            return res;
        }

        public async Task<bool> DeleteCountry(long Id)
        {
            var country = _context.Countries.Where(x => x.Id == Id && x.IsDeleted != true).FirstOrDefault();
            if (country == null)
            {
                return false;
            }
            _context.Countries.Remove(country);
            return true;
        }
    }
}
