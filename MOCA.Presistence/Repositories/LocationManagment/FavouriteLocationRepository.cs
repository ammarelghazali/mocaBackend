using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class FavouriteLocationRepository : GenericRepository<FavouriteLocation>, IFavouriteLocationRepository
    {
        private readonly ApplicationDbContext _context;
        public FavouriteLocationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsFavouriteLocation(long LocationID, long BasicUserID)
        {
            var data = _context.FavouriteLocations.Where(c => c.IsDeleted != true && c.BasicUserId == BasicUserID && c.LocationId == LocationID).FirstOrDefault();
            if (data != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteFavouriteLocation(long LocationID, long BasicUserID)
        {
            var data = _context.FavouriteLocations.Where(c => c.IsDeleted != true && c.BasicUserId == BasicUserID && c.LocationId == LocationID).FirstOrDefault();
            if (data != null)
            {
                return false;
            }
            _context.FavouriteLocations.Remove(data);
            return true;
        }
    }
}
