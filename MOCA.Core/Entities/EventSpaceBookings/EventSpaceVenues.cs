using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class EventSpaceVenues : BaseEntity
    {
        [Required]
        public long EventSpaceBookingId { get; set; }

        [MaxLength(450)]
        [Required]
        public string VenueName { get; set; }

        [ForeignKey("EventSpaceBookingId")]
        public EventSpaceBooking EventSpaceBooking { get; set; }

    }
}
