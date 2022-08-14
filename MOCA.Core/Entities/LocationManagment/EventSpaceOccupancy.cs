using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.DynamicLists;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class EventSpaceOccupancy : BaseEntity
    {
        public long EventSpaceId { get; set; }
        [ForeignKey("EventSpaceId")]
        public virtual EventSpace EventSpace { get; set; }
        public long VenueSetupId { get; set; }
        [ForeignKey("VenueSetupId")]
        public virtual VenueSetup VenueSetup { get; set; }
        public int MaximumOccupancy { get; set; }
        public int CovidOccupancy { get; set; }
    }
}
