using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.MocaSetting
{
    public class PlanType : BaseEntity
    {
        [Required, MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string URL { get; set; }

    }
}
