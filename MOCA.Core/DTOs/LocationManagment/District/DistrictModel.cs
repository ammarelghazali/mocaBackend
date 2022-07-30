using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.LocationManagment.District
{
    public class DistrictModel
    {
        public long Id { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "City Id Cannot Be 0")]
        public long CityId { get; set; }
        [Required]
        public string DistrictName { get; set; }
    }
}
