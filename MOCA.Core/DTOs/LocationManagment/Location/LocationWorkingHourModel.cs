using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.LocationManagment.Location
{
    public class LocationWorkingHourModel
    {
        public long Id { get; set; }
        [Required]
        public string StartWorkingHour { get; set; }
        [Required]
        public string EndWorkingHour { get; set; }
        [Required]
        public string DayFrom { get; set; }
        [Required]
        public string DayTo { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "Location Id Cannot Be 0")]
        public long LocationId { get; set; }
    }
}
