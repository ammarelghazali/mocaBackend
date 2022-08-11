using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.LocationManagment
{
    public class Amenity :  BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }

        public ICollection<SpaceAmenity> SpaceAmenities { get; set; }


    }
}
    