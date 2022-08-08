using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.LocationManagment;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.DynamicLists
{
    public class WorkSpaceType : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public long WorkSpaceCategoryId { get; set; }

        [ForeignKey("WorkSpaceCategoryId")]
        public WorkSpaceCategory WorkSpaceCategory { get; set; }

        public ICollection<WorkSpace> WorkSpaces { get; set; }
    }
}
