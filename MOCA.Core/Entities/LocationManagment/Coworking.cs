using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class Coworking : BaseEntity
    {
        [Required]
        public long LocationId { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
        public int Occupancy { get; set; }
        public int RemainingOccupancy { get; set; }

        public ICollection<CoWorkingSpaceHourlyPricing> CoWorkingSpaceHourlyPricings { get; set; }
        public ICollection<CoworkingSpaceTailoredPricing> CoworkingSpaceTailoredPricings { get; set; }
        public ICollection<CoworkingSpaceBundlePricing> CoworkingSpaceBundlePricings { get; set; }
    }
}
