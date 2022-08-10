using MOCA.Core.Entities.WorkSpaceReservations.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces
{
    public class WorkSpaceHourlyTopUp : BaseWorkSpaceTopUp
    {
        [Required]
        public long HourId { get; set; }
        // TODO: Reference to LoungeLocationPricing

        [Required]
        public decimal HourlyTotalPrice { get; set; }

        [Required]
        public long WorkSpaceReservationHourlyId { get; set; }

        [ForeignKey("WorkSpaceReservationHourlyId")]
        public WorkSpaceReservationHourly WorkSpaceReservation { get; set; }
    }
}
