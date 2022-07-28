using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.MocaSetting
{
    public class Severity : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
