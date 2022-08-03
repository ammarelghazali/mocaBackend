using MOCA.Core.Entities.WorkSpaceReservations.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations
{
    public class WorkSpaceHourlyTopUp :  BaseWorkSpaceTopUp
    {
        [Required]
        public long HourId { get; set; }

        [Required]
        public decimal HourlyTotalPrice { get; set; }

        [Required]
        public long WorkSpaceReservationHourlyId { get; set; }

        [ForeignKey("WorkSpaceReservationHourlyId")]
        public WorkSpaceReservationHourly WorkSpaceReservation { get; set; }
    }
}
