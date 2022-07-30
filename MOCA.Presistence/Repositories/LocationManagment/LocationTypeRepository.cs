using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class LocationTypeRepository : Repository<LocationType>, ILocationTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public LocationTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteLocationType(long Id)
        {
            var locationType = _context.LocationTypes.Where(x => x.Id == Id && x.IsDeleted != true).FirstOrDefault();
            if (locationType == null)
            {
                return false;
            }
            _context.LocationTypes.Remove(locationType);
            return true;
        }
    }
}
