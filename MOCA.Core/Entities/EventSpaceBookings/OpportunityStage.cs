using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class OpportunityStage : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<OpportunityStageReport> OpportunityStageReports { get; set; }
    }
}
