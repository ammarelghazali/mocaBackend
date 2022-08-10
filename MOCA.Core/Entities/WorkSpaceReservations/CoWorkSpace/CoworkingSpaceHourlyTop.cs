using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Entities.WorkSpaceReservations.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations.CoWorkSpace
{
    public class CoworkingSpaceHourlyTop : BaseWorkSpaceTopUp
    {
        [Required]
        public long HourId { get; set; }

        [ForeignKey("HourId")]
        public CoWorkingSpaceHourlyPricing CoWorkingSpaceHourlyPricing { get; set; }

        [Required]
        public decimal HourlyTotalPrice { get; set; }

        [Required]
        public long CoworkingSpaceReservationHourlyId { get; set; }

        [ForeignKey("CoworkingSpaceReservationHourlyId")]
        public CoworkingSpaceReservationHourly CoworkingSpaceReservation { get; set; }
    }
}
