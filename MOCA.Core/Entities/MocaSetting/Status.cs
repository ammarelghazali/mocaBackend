using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.MocaSetting
{
    public class Status : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
    }
}
