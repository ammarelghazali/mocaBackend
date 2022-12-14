
using System.ComponentModel.DataAnnotations;
using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.MocaSetting
{
    public class CaseType : BaseEntity
    {
        [Required, MaxLength(150)]
        public string Name { get; set; }
    }
}
