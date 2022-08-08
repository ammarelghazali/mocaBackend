using MOCA.Core.Entities.LocationManagment.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class EventSpaceHourlyPricing : BaseHourlyPricingEntity
    {
        [Required]
        public long EventSpaceId { get; set; }

        [ForeignKey("EventSpaceId")]
        public EventSpace EventSpace { get; set; }
    }
}
