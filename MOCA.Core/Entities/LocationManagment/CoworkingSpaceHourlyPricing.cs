using MOCA.Core.Entities.LocationManagment.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class CoWorkingSpaceHourlyPricing : BaseWorkSpaceHourlyPricing
    {
        [Required]
        public long CoworkingId { get; set; }

        [ForeignKey("CoworkingId")]
        public Coworking Coworking { get; set; }

    }
}
