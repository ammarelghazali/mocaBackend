using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.Events.Repositories
{
    public interface IContactDetailsRepository : IGenericRepository<ContactDetails>
    {
        Task<ContactDetails> CheckEmailAndMobileExist(string email, string mobile);
        Task<bool> DeleteContact_DetailByID(long EventsOpportunities_ID);
        Task<IList<ContactDetails>> GetAllContact_DetailByOpportunitiyID(long EventsOpportunities_ID);
        Task<ContactDetails> GetContact_DetailByID(long Id);
        Task<ContactDetails> GetContact_DetailByEmail(string Email);
    }
}
