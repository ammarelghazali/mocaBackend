using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.LocationManagment.Building
{
    public class BuildingModel
    {
        public long Id { get; set; }
        [Required]
        public long LocationId { get; set; }
        public string Name { get; set; }
        public decimal GrossArea { get; set; }
        public decimal NetArea { get; set; }
        public int MaleRestroomCount { get; set; }
        public int FemaleRestroomCount { get; set; }
    }
}
