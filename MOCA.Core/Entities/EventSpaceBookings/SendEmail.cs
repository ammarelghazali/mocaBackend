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
        public long ContactDetailId { get; set; }
        public long? BookATourId { get; set; }
        public long? EventSpaceBookingId { get; set; }
        public long? EmailTemplateId { get; set; }

        [ForeignKey("EventSpaceBookingId")]
        public EventSpaceBooking EventSpaceBooking { get; set; }

        [ForeignKey("ContactDetailId")]
        public ContactDetails ContactDetail { get; set; }
    }
}
