using MOCA.Core.Entities.EventSpaceBookings;

namespace MOCA.Core.Interfaces.Events
{
    public interface IEmailTemplateRepository
    {
        Task<EmailTemplate> GetLatestEmailTemplate(int emailTypeID);
        Task<EmailTemplate> AddAsync(EmailTemplate emailTemplate);
    }
}
