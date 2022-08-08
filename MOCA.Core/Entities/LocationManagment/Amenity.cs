using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.LocationManagment
{
    public class Amenity :  BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public ICollection<SpaceAmenity> SpaceAmenities { get; set; }
    }
}
    