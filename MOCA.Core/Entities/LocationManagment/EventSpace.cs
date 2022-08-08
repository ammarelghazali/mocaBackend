using MOCA.Core.Entities.LocationManagment.Base;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.LocationManagment
{
    public class EventSpace : BaseSpaceEntity
    {
        [Required]
        public decimal GrossArea { get; set; }

        [Required]
        public decimal NetArea { get; set; }

        [Required]
        public int Type { get; set; }

        public string? TermsOfUse { get; set; }

        [Required]
        public string Url360Tour { get; set; }

        [Required]
        public string UnitEBrochure { get; set; }

        public int? RestRoomMaleOccupancy { get; set; } 
        public int? RestRoomFemaleOccupancy { get; set; }

        public ICollection<EventSpaceHourlyPricing> EventSpaceHourlyPricings { get; set; }
        public ICollection<EventSpaceOccupancy> EventSpaceOccupancies { get; set; }
    }
}
