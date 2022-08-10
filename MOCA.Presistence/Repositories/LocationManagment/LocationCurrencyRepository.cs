using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class LocationCurrencyRepository : GenericRepository<LocationCurrency>, ILocationCurrencyRepository
    {
        private readonly ApplicationDbContext _context;
        public LocationCurrencyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteByLocationID(long LocationID)
        {
            var locationCurrencies = _context.LocationCurrencies.Where(x => x.LocationId == LocationID && x.IsDeleted != true).FirstOrDefault();
            if (locationCurrencies == null)
            {
                return false;
            }
            _context.LocationCurrencies.Remove(locationCurrencies);
            return true;
        }

        public async Task<List<LocationCurrency>> GetByLocationID(long LocationID)
        {
            var locationCurrencies = _context.LocationCurrencies.Where(x => x.LocationId == LocationID && x.IsDeleted != true).ToList();
            if (locationCurrencies == null)
            {
                return null;
            }
            return locationCurrencies;
        }

        public async Task<bool> CheckLocationCurrencyIsUinque(long LocationId, long CurrencyId)
        {
            var locationName = _context.LocationCurrencies.Where(x => x.LocationId == LocationId && x.CurrencyId == CurrencyId && x.IsDeleted != true).FirstOrDefault();
            if (locationName == null)
            {
                return true;
            }
            return false;
        }
    }
}
