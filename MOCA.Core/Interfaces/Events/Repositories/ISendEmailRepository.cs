using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.Events.Repositories
{
    public interface ISendEmailRepository : IGenericRepository<SendEmail>
    {
        Task<IList<SendEmail>> GetEmailHistoryByOpportunitiyID(long OpportunitiyID);
    }
}
