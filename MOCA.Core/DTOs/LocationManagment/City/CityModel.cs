using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.LocationManagment.City
{
    public class CityModel
    {
        public long Id { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "Country Id Cannot Be 0")]
        public long CountryId { get; set; }
        [Required]
        public string CityName { get; set; }
    }


}
