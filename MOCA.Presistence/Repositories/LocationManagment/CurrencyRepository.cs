
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;
using System.Data.Entity;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {
        private readonly ApplicationDbContext _context;
        public CurrencyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Currency> CurrencyIsExists(long CurrencyID)
        {
            if (CurrencyID <= 0) { return null; }

            var res = await _context.Currencies.FirstOrDefaultAsync(x => x.Id.Equals(CurrencyID));
            return res;
        }

        public async Task<bool> HasAnyRelatedEntities(long CurrencyID)
        {
            if (CurrencyID <= 0) return false;
            return await _context.Locations.AnyAsync(x => x.CurrencyId.Equals(CurrencyID));
        }

        public async Task<bool> DeleteCurrency(long Id)
        {
            var currency = _context.Currencies.Where(x => x.Id == Id && x.IsDeleted != true).FirstOrDefault();
            if (currency == null)
            {
                return false;
            }
            _context.Currencies.Remove(currency);
            return true;
        }
    }
}
