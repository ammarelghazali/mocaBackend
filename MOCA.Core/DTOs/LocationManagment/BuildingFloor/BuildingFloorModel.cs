using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.LocationManagment.BuildingFloor
{
    public class BuildingFloorModel
    {
        public long Id { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Building Id Cannot Be 0")]
        public long BuildingId { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Gross Area Cannot Be 0")]
        public decimal GrossArea { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Net Area Cannot Be 0")]
        public decimal NetArea { get; set; }
        [Required]
        public int MaleRestroomCount { get; set; }
        [Required]
        public int FemaleRestroomCount { get; set; }
        public bool InstallAccessPoint { get; set; }
    }
}
