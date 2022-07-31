using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Events.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.Events
{
    public class EventSpaceTimesRepository : GenericRepository<EventSpaceTime>, IEventSpaceTimesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EventSpaceTimesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> BookingEventSpaceTime(List<EventSpaceTime> eventSpaceTimes)
        {
            await _dbContext.Set<EventSpaceTime>().AddRangeAsync(eventSpaceTimes);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<EventSpaceTime>> GetBookedEventSpaceTimeById(long bookedEventSpaceID)
        {
            return await _dbContext.EventSpaceTimes
                                   .Where(time => time.BookEventSpace_ID == bookedEventSpaceID && time.IsDeleted != true).ToListAsync();
        }

        public async Task<bool> DeleteByBookEventSpace_ID(long BookEventSpace_ID)
        {
            var data = await _dbContext.EventSpaceTimes.Where(time => time.BookEventSpace_ID == BookEventSpace_ID
                                                                    && time.IsDeleted != true)
                                                        .FirstOrDefaultAsync();
            if (data != null)
            {
                _dbContext.EventSpaceTimes.Remove(data);
            }
            return true;
        }
    }
}
