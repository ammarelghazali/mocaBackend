using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class LocationBankAccountRepository : GenericRepository<LocationBankAccount>, ILocationBankAccountRepository
    {
        private readonly ApplicationDbContext _context;
        public LocationBankAccountRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteByLocationID(long LocationID)
        {
            var locationBankAccount = _context.LocationBankAccounts.Where(x => x.LocationId == LocationID && x.IsDeleted != true).FirstOrDefault();
            if (locationBankAccount == null)
            {
                return false;
            }
            _context.LocationBankAccounts.Remove(locationBankAccount);
            return true;
        }

        public async Task<LocationBankAccount> GetByLocationID(long LocationID)
        {
            var locationBankAccount = _context.LocationBankAccounts.Where(x => x.LocationId == LocationID && x.IsDeleted != true).FirstOrDefault();
            if (locationBankAccount == null)
            {
                return null;
            }
            return locationBankAccount;
        }
    }
}
