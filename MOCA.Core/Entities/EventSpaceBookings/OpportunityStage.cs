using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class OpportunityStage : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<OpportunityStageReport> OpportunityStageReports { get; set; }
    }
}
