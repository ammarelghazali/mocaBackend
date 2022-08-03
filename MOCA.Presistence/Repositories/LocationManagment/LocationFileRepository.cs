using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class LocationFileRepository : GenericRepository<LocationFile>, ILocationFileRepository
    {
        private readonly ApplicationDbContext _context;
        public LocationFileRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAllLocationFileByLocationID(long LocationID)
        {
            var LocationFiles = _context.LocationFiles.Where(x => x.LocationId == LocationID && x.IsDeleted != true).ToList();

            _context.LocationFiles.RemoveRange(LocationFiles);
            return true;
        }

        public async Task<List<LocationFile>> GetAllLocationFileByLocationID(long LocationID)
        {
            var LocationFiles = _context.LocationFiles.Where(x => x.LocationId == LocationID && x.IsDeleted != true).ToList();
            return new List<LocationFile>(LocationFiles);
        }
    }
}
