using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Events.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.Events
{
    public class InitiatedRepository : GenericRepository<Initiated>, IInitiatedRepository
    {
        private readonly ApplicationDbContext _context;
        public InitiatedRepository(ApplicationDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
