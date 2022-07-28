using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class EventSpaceVenues : BaseEntity
    {
        public long EventSpaceBookingId { get; set; }
        public string VenueName { get; set; }

        [ForeignKey("EventSpaceBookingId")]
        public EventSpaceBooking EventSpaceBooking { get; set; }

    }
}
