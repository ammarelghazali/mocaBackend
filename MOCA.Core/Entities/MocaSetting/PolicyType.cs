using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.MocaSetting
{
    public class PolicyType : BaseEntity
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        
        public Policy Policy { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string URL { get; set; }
    }
}
