using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.LocationManagment.Building
{
    public class GetBuildingModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Gross Area Cannot Be 0")]
        public decimal GrossArea { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Net Area Cannot Be 0")]
        public decimal NetArea { get; set; }
        public int TotalFloors { get; set; }
        public bool InstallAccessPoint { get; set; }
    }
}
