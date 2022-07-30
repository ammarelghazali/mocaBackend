using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.Events.Repositories
{
    public interface IEventSpaceTimesRepository : IGenericRepository<EventSpaceTime>
    {
        Task<int> BookingEventSpaceTime(List<EventSpaceTime> eventSpaceTimes);
        Task<List<EventSpaceTime>> GetBookedEventSpaceTimeById(long bookedEventSpaceID);
        Task<bool> DeleteByBookEventSpace_ID(long BookEventSpace_ID);
    }
}
