using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class EventReccurance : BaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        public ICollection<EventSpaceBooking> EventSpaceBookings { get; set; }
    }
}
