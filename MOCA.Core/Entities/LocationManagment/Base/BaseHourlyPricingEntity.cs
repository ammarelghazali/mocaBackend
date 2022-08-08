using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment.Base
{
    public class BaseHourlyPricingEntity : BaseEntity
    {
        [Required]
        public int Hours { get; set; }

        [Required]
        public int PricePerHour { get; set; }

        [Required]
        public int TotalPrice { get; set; }

        [Required]
        public long MemberTypeId { get; set; }

        [ForeignKey("MemberTypeId")]
        public MemberType MemberType { get; set; }
    }
}
