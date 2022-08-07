using MOCA.Core.Entities.LocationManagment.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class WorkSpace : BaseSpaceEntity
    {
        public int? GrossArea { get; set; }

        public int? NetArea { get; set; }

        [Required]
        public long WorkSpaceTypeId { get; set; }

        [ForeignKey("WorkSpaceTypeId")]
        public WorkSpaceType WorkSpaceType { get; set; }

        [Required]
        public int MaximumOccupancy { get; set; }

        [Required]
        public bool IsFurnishing { get; set; }
    }
}
