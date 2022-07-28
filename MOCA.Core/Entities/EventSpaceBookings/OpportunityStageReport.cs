using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class OpportunityStageReport : BaseEntity
    {
        public DateTime Date { get; set; }
        public long EventSpaceBookingId { get; set; }
        public string? Comment { get; set; }
        public DateTime? Reminder { get; set; }
        public long OpportunityId { get; set; }

        [ForeignKey("EventSpaceBookingId")]
        public EventSpaceBooking EventSpaceBooking { get; set; }

        [ForeignKey("OpportunityStageId")]
        public OpportunityStage OpportunityStage { get; set; }
    }
}
