using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.MocaSetting
{
    public class Severity : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
    }
}
