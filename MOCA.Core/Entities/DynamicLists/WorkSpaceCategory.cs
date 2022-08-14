using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.DynamicLists
{ 
    public class WorkSpaceCategory : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public ICollection<WorkSpaceType> WorkSpaceTypes { get; set; }
    }
}
