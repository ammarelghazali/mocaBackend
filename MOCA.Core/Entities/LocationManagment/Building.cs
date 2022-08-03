using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class Building : BaseEntity
    {
        public long LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal GrossArea { get; set; }
        [Required]
        public decimal NetArea { get; set; }
        public bool InstallAccessPoint { get; set; }
        public ICollection<BuildingFloor> BuildingFloors { get; set; }
    }
}
