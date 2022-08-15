using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.DynamicLists;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class LocationInclusion : BaseEntity
    {
        public long LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
        public long AmenityId { get; set; }
        [ForeignKey("AmenityId")]
        public virtual Amenity Amenity { get; set; }
    }
}
