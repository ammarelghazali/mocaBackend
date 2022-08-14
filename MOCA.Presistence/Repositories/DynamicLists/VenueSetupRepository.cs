using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Interfaces.DynamicLists.Repositories;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.DynamicLists
{
    public class VenueSetupRepository : GenericRepository<VenueSetup>, IVenueSetupRepository

    {
        private readonly ApplicationDbContext _context;
        public VenueSetupRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteVenueSetup(long Id)
        {
            var venuesetup = _context.VenueSetups.Where(x => x.Id == Id && x.IsDeleted == false).FirstOrDefault();
            if (venuesetup == null)
            {
                return false;
            }
            _context.VenueSetups.Remove(venuesetup);
            return true;
        }

        public async Task<bool> IsUniqueNameAsync(string setup)
        {
            var venuesetup = _context.VenueSetups.Where(x => x.Name.Equals(setup) && x.IsDeleted != true).FirstOrDefault();
            if (venuesetup == null)
            {
                return true;
            }
            return false;

        }
    }
}
