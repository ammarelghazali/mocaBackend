using MOCA.Core.Entities.LocationManagment.Base;
using MOCA.Core.Entities.MeetingSpaceReservation;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.LocationManagment
{
    public class MeetingSpace : BaseSpaceEntity
    {
        [Required]
        public decimal GrossArea { get; set; }

        [Required]
        public decimal NetArea { get; set; }

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
        public ICollection<MeetingReservation> MeetingReservations { get; set; }
    }
}
