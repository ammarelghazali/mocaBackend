using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        private readonly ApplicationDbContext _context;
        public LocationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckLocationNameIsUinque(string LocationName)
        {
            var locationName = _context.Locations.Where(x => x.Name == LocationName && x.IsDeleted != true).FirstOrDefault();
            if (locationName == null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteLocation(long Id)
        {
            var location = _context.Locations.Where(x => x.Id == Id && x.IsDeleted != true).FirstOrDefault();
            if (location == null)
            {
                return false;
            }
            _context.Locations.Remove(location);
            return true;
        }

        public async Task<List<long>> GetAllDistinictDistrict()
        {
            var locationDistricts = _context.Locations.Where(x => x.IsDeleted != true).Select(x => x.DistrictId).Distinct().ToList();
            if (locationDistricts == null)
            {
                return new List<long>(null);
            }
            return new List<long>(locationDistricts);
        }
    }
}
