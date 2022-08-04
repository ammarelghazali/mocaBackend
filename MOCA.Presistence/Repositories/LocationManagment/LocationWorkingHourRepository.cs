using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class LocationWorkingHourRepository : GenericRepository<LocationWorkingHour>, ILocationWorkingHourRepository
    {
        private readonly ApplicationDbContext _context;
        public LocationWorkingHourRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAllLocationWorkingHourByLocationID(long LocationID)
        {
            var LocationWorkingHours = _context.LocationWorkingHours.Where(x => x.LocationId == LocationID && x.IsDeleted != true).ToList();

            _context.LocationWorkingHours.RemoveRange(LocationWorkingHours);
            return true;
        }

        public async Task<List<LocationWorkingHour>> GetAllLocationWorkingHourByLocationID(long LocationID)
        {
            var LocationWorkingHours = _context.LocationWorkingHours.Where(x => x.LocationId == LocationID && x.IsDeleted != true).ToList();
            return new List<LocationWorkingHour>(LocationWorkingHours);
        }
    }
}
