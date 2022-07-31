using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class LocationImageRepository : GenericRepository<LocationImage>, ILocationImageRepository
    {
        private readonly ApplicationDbContext _context;
        public LocationImageRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAllLocationImageByLocationID(long LocationID)
        {
            var LocationImages = _context.LocationImages.Where(x => x.LocationId == LocationID && x.IsDeleted != true).ToList();

            _context.LocationImages.RemoveRange(LocationImages);
            return true;
        }

        public async Task<List<LocationImage>> GetAllLocationImageByLocationID(long LocationID)
        {
            var LocationImages = _context.LocationImages.Where(x => x.LocationId == LocationID && x.IsDeleted != true).ToList();
            return new List<LocationImage>(LocationImages);
        }
    }
}
