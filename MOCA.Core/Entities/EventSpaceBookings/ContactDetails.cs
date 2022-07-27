using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class ContactDetails : BaseEntity
    {
        public long? BookATour_ID { get; set; }

        [MaxLength(450)]
        public string Name { get; set; }

        [MaxLength(450)]
        public string? Email { get; set; }

        [MaxLength(450)]
        public string? MobileNumber { get; set; }
        public long? EventsOpportunities_ID { get; set; }

        public ICollection<SendEmail> SendEmails { get; set; }

        [ForeignKey("EventsOpportunities_ID")]
        public EventSpaceBooking EventSpace_Booking { get; set; }
    }
}
