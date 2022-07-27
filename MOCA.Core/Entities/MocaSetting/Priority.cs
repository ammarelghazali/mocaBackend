
using System.ComponentModel.DataAnnotations;
using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.MocaSetting
{
    public class Priority : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
