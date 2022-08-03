using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.Events.Repositories
{
    public interface IOpportunityStageReportRepository : IGenericRepository<OpportunityStageReport>
    {
        Task<List<OpportunityStageReport>> GetReportByIDs(long Opportunity_ID, long OpportunityStage_ID);
        Task<List<OpportunityStageReport>> GetReportByIDs(long Opportunity_ID);
    }
}
