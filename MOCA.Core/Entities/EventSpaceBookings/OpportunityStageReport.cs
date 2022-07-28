using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class OpportunityStageReport : BaseEntity
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public long EventSpaceBookingId { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }
        public DateTime? Reminder { get; set; }
        public long OpportunityStageId { get; set; }

        [ForeignKey("EventSpaceBookingId")]
        public EventSpaceBooking EventSpaceBooking { get; set; }

        [ForeignKey("OpportunityStageId")]
        public OpportunityStage OpportunityStage { get; set; }
    }
}
