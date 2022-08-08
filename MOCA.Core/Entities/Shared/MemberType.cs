using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.Shared
{
    public class MemberType : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
