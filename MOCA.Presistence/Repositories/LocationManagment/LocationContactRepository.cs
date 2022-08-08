using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class LocationContactRepository : GenericRepository<LocationContact>, ILocationContactRepository
    {
        private readonly ApplicationDbContext _context;
        public LocationContactRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAllLocationContactByLocationID(long LocationID)
        {
            var LocationContacts = _context.LocationContacts.Where(x => x.LocationId == LocationID && x.IsDeleted != true).ToList();

            _context.LocationContacts.RemoveRange(LocationContacts);
            return true;
        }

        public async Task<List<LocationContact>> GetAllLocationContactByLocationID(long LocationID)
        {
            var LocationContacts = _context.LocationContacts.Where(x => x.LocationId == LocationID && x.IsDeleted != true).ToList();
            return new List<LocationContact>(LocationContacts);
        }
    }
}
