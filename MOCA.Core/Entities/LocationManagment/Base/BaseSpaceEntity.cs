using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment.Base
{
    public class BaseSpaceEntity : BaseEntity
    {
        [Required]
        public string UnitNumber { get; set; }

        [Required]
        public bool InstallAccessPoint { get; set; }
        public string? Description { get; set; }

        [Required]
        public long BuildingFloorId { get; set; }

        [ForeignKey("BuildingFloorId")]
        public BuildingFloor BuildingFloor { get; set; }
    }
}
