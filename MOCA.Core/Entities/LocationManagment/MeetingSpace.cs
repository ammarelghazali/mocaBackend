using MOCA.Core.Entities.LocationManagment.Base;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.LocationManagment
{
    public class MeetingSpace : BaseSpaceEntity
    {
        [Required]
        public int GrossArea { get; set; }

        [Required]
        public int NetArea { get; set; }

        [Required]
        public string VenueName { get; set; }

        public string? TermsOfUse { get; set; }

        [Required]
        public bool IsFurnishing { get; set; }

        [Required]
        public int MaximumOccupancy { get; set; }
   
        public int? CovidOccupancy { get; set; }

        [Required]
        public string Url360Tour { get; set; }

        [Required]
        public string UnitEBrochure { get; set; }   

        public ICollection<MeetingSpaceHourlyPricing> MeetingSpaceHourlyPricings { get; set; }
    }
}
