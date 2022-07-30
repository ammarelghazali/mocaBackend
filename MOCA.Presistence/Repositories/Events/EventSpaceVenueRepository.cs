using Microsoft.EntityFrameworkCore;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Events.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.Events
{
    public class EventSpaceVenueRepository : GenericRepository<EventSpaceVenues>, IEventSpaceVenueRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EventSpaceVenueRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddEventSpaceVenues(List<EventSpaceVenues> eventSpaceVenues)
        {
            await _dbContext.Set<EventSpaceVenues>().AddRangeAsync(eventSpaceVenues);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<EventSpaceVenues>> GetEventSpaceVenuesById(long bookEventSpaceID)
        {
            return await _dbContext.EventSpaceVenues
                                   .Where(venue => venue.EventSpaceBookingId == bookEventSpaceID && venue.IsDeleted != true).ToListAsync();
        }

        public async Task<List<DropdownViewModel>> GetAllDistinctVenue(long BookEventSpace_ID)
        {
            List<DropdownViewModel> dropdownViewModels = new List<DropdownViewModel>();
            var data = await _dbContext.EventSpaceVenues.Where(x => x.EventSpaceBookingId == BookEventSpace_ID).Select(c => c.VenueName).Distinct().ToListAsync();
            foreach (var venue in data)
            {
                DropdownViewModel dropdownViewModel = new DropdownViewModel();
                dropdownViewModel.Name = venue;
                dropdownViewModels.Add(dropdownViewModel);
            }
            return dropdownViewModels;
        }

        public async Task<bool> DeleteByBookEventSpace_ID(long BookEventSpace_ID)
        {
            var data = await _dbContext.EventSpaceVenues.Where(ven => ven.EventSpaceBookingId == BookEventSpace_ID && ven.IsDeleted != true)
                                                         .FirstOrDefaultAsync();
            if (data != null)
            {
                _dbContext.EventSpaceVenues.Remove(data);
            }
            return true;
        }
    }
}
