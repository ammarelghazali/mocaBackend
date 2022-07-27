using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class SendEmail : BaseEntity
    {
        public string? CC { get; set; }
        public string FromUser { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public long ContactDetails_ID { get; set; }
        public long? BookATour_ID { get; set; }
        public long? EventsOpportunities_ID { get; set; }
        public long? EmailTemplate_ID { get; set; }

        [ForeignKey("EventsOpportunities_ID")]
        public EventSpaceBooking EventSpace_Booking { get; set; }

        [ForeignKey("ContactDetails_ID")]
        public ContactDetails ContactDetail { get; set; }
    }
}
