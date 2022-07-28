using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.EventSpaceBookings;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.LocationManagment
{
    public class Industry : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<EventSpaceBooking> EventSpaceBookings { get; set; }
        public ICollection<LocationIndustry> LocationIndustrys { get; set; }
    }
}
