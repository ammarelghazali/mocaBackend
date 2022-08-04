using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.LocationManagment.Location
{
    public class LocationImageModel
    {
        public long Id { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "Location Id Cannot Be 0")]
        public long LocationId { get; set; }
        [Required]
        public string LocationImageFilePath { get; set; }
    }
}
