using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.MocaSetting
{
    public class TopUpType : BaseEntity
    {
        [Required, MaxLength(500)]
        public string Name { get; set; }

        [Required]
        public string URL { get; set; }
    }
}
