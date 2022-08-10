using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.LocationManagment.Location
{
    public class LocationContactModel
    {
        public long Id { get; set; }
        //[Range(1, long.MaxValue, ErrorMessage = "Location Id Cannot Be 0")]
        public long LocationId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Department { get; set; }
        //[Required]
        public string CountryCode { get; set; }
        [Required]
        public string Position { get; set; }
    }
}
