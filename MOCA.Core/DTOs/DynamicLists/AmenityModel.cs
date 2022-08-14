using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.DynamicLists
{
    public class AmenityModel
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Icon { get; set; }

        // public ICollection<SpaceAmenity> SpaceAmenities { get; set; }
    }
}
