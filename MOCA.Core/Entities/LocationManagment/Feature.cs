using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.LocationManagment
{
    public class Feature : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public ICollection<Furnishing> Furnishings { get; set; }
        public ICollection<SpaceAmenity> SpaceAmenities { get; set; }
        public ICollection<MarketingImages> MarketingImages { get; set; }
    }
}
