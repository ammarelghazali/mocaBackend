using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.MocaSetting
{
    public class PlanType : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string URL { get; set; }

    }
}
