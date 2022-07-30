using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Events.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.Events
{
    public class OpportunityStageReportRepository : GenericRepository<OpportunityStageReport>, IOpportunityStageReportRepository
    {
        private readonly ApplicationDbContext _context;
        public OpportunityStageReportRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<OpportunityStageReport>> GetReportByIDs(long Opportunity_ID, long OpportunityStage_ID)
        {
            var data = _context.OpportunityStageReports.Where(x => x.EventSpaceBookingId == Opportunity_ID
                                                                && x.OpportunityStageId == OpportunityStage_ID && x.IsDeleted != true);
            return data.ToList();
        }

        public async Task<List<OpportunityStageReport>> GetReportByIDs(long Opportunity_ID)
        {
            var data = _context.OpportunityStageReports.Where(x => x.EventSpaceBookingId == Opportunity_ID
                                                                && x.IsDeleted != true);
            return data.ToList();
        }
    }
}
