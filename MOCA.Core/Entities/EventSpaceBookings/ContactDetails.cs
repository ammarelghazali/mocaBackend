using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class ContactDetails : BaseEntity
    {
        public long? BookATourId { get; set; }

        [Required]
        [MaxLength(450)]
        public string Name { get; set; }

        [Required]
        [MaxLength(450)]
        public string? Email { get; set; }

        [MaxLength(450)]
        public string? MobileNumber { get; set; }

        public long? EventSpaceBookingId { get; set; }

        public ICollection<SendEmail> SendEmails { get; set; }

        [ForeignKey("EventSpaceBookingId")]
        public EventSpaceBooking EventSpaceBooking { get; set; }
    }
}

