using MOCA.Core.Entities.LocationManagment.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class MeetingSpaceHourlyPricing : BaseHourlyPricingEntity
    {
        [Required]
        public long MeetingSpaceId { get; set; }

        [ForeignKey("MeetingSpaceId")]
        public MeetingSpace MeetingSpace { get; set; }
    }
}
