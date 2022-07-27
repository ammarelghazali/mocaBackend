using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class OpportunityStageReport : BaseEntity
    {
        public DateTime Date { get; set; }
        public long OpportunityStage_ID { get; set; }
        public string? Comment { get; set; }
        public DateTime? Reminder { get; set; }
        public long Opportunity_ID { get; set; }

        [ForeignKey("Opportunity_ID")]
        public EventSpaceBooking EventSpace_Booking { get; set; }

        [ForeignKey("OpportunityStage_ID")]
        public OpportunityStage OpportunityStage { get; set; }
    }
}
