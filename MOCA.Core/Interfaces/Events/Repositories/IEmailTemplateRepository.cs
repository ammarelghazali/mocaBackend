using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.Events
{
    public interface IEmailTemplateRepository : IGenericRepository<EmailTemplate>
    {
        Task<EmailTemplate> GetLatestEmailTemplate(int emailTypeID);
    }
}
