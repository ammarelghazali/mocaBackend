using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Events.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.Events
{
    public class SendEmailRepository : GenericRepository<SendEmail>, ISendEmailRepository
    {
        private readonly ApplicationDbContext _context;
        public SendEmailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IList<SendEmail>> GetEmailHistoryByOpportunitiyID(long OpportunitiyID)
        {
            return _context.SendEmails.Where(x => x.EventSpaceBookingId == OpportunitiyID && x.IsDeleted != true).ToList();
        }
    }
}
