using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.MocaSetting
{
    public class Status : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
