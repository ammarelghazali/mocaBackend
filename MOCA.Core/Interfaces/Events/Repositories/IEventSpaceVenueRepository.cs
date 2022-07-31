using MOCA.Core.DTOs.Shared;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.Events.Repositories
{
    public interface IEventSpaceVenueRepository : IGenericRepository<EventSpaceVenues>
    {
        Task<int> AddEventSpaceVenues(List<EventSpaceVenues> eventSpaceVenues);
        Task<List<EventSpaceVenues>> GetEventSpaceVenuesById(long bookedEventSpaceVenue);
        Task<List<DropdownViewModel>> GetAllDistinctVenue(long BookEventSpace_ID);
        Task<bool> DeleteByBookEventSpace_ID(long BookEventSpace_ID);
    }
}
