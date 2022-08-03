using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class SendEmail : BaseEntity
    {
        public string? CC { get; set; }

        [Required]
        public string FromUser { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public long ContactDetailId { get; set; }
        public long? BookATourId { get; set; }
        public long? EventSpaceBookingId { get; set; }
        public long? EmailTemplateId { get; set; }

        [ForeignKey("EmailTemplateId")]
        public EmailTemplate EmailTemplate { get; set; }

        [ForeignKey("EventSpaceBookingId")]
        public EventSpaceBooking? EventSpaceBooking { get; set; }

        [ForeignKey("ContactDetailId")]
        public ContactDetails ContactDetail { get; set; }
    }
}
