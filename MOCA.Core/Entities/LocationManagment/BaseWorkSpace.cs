using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Entities.LocationManagment.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class BaseWorkSpace : BaseSpaceEntity
    {
        public decimal? GrossArea { get; set; }
        public decimal? NetArea { get; set; }

        [Required]
        public long WorkSpaceCategoryId { get; set; }
        [ForeignKey("WorkSpaceCategoryId")]
        public WorkSpaceCategory WorkSpaceCategory { get; set; }

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
