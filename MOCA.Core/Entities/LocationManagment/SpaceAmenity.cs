using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class SpaceAmenity : BaseEntity
    {
        [Required]
        public long SpaceId { get; set; }

        [Required]
        public long AmenityId { get; set; }

        [ForeignKey("AmenityId")]
        public Amenity Amenity { get; set; }

        [Required]
        public long FeatureId { get; set; }

        [ForeignKey("FeatureId")]
        public Feature Feature { get; set; }

    }
}
