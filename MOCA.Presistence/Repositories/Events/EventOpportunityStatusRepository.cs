using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Events.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.Events
{
    public class EventOpportunityStatusRepository : GenericRepository<EventOpportunityStatus>,
                                                            IEventOpportunityStatusRepository
    {
        private readonly ApplicationDbContext _context;
        public EventOpportunityStatusRepository(ApplicationDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
