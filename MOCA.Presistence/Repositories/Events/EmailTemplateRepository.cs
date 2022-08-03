using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Events;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.Events
{
    public class EmailTemplateRepository : GenericRepository<EmailTemplate>, IEmailTemplateRepository
    {
        private readonly ApplicationDbContext _context;
        public EmailTemplateRepository(ApplicationDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<EmailTemplate> GetLatestEmailTemplate(int emailTypeID)
        {
            return _context.EmailTemplates.Where(emailTemp => emailTemp.EmailTemplateTypeID == emailTypeID)
                                                .OrderByDescending(emailTemp => emailTemp.CreatedAt)
                                                .FirstOrDefault();
        }
    }
}
