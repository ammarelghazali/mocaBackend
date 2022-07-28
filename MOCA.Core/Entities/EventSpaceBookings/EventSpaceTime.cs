using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class EventSpaceTime : BaseEntity
    {
        public long EventSpaceBookingId { get; set; }
        public DateTime? RecurrenceStartDate { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }
        public string? RecurrenceStartTime { get; set; }
        public string? RecurrenceEndTime { get; set; }
        public string? RecurrenceDay { get; set; }

        [ForeignKey("EventSpaceBookingId")]
        public EventSpaceBooking EventSpaceBooking { get; set; }

    }
}
