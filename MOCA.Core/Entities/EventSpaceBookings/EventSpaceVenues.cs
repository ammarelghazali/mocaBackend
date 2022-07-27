using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class EventSpaceVenues : BaseEntity
    {
        public long BookEventSpace_ID { get; set; }
        public string VenueName { get; set; }

        [ForeignKey("BookEventSpace_ID")]
        public EventSpaceBooking EventSpace_Booking { get; set; }

    }
}
