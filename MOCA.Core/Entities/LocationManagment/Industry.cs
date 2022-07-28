using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.EventSpaceBookings;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.LocationManagment
{
    public class Industry : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public IList<EventSpaceBooking> EventSpaceBookings { get; set; }

    }
}
