using MOCA.Core.Entities.Shared.Reservations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces
{
    public class WorkSpaceHourlyCancellation
    {
        [Key, Column(Order = 1)]
        public long WorkSpaceHourlyReservationId { get; set; }

        [ForeignKey("WorkSpaceHourlyReservationId")]
        public WorkSpaceReservationHourly WorkSpaceReservationHourly { get; set; }

        [Key, Column(Order = 2)]
        public long CancellationId { get; set; }

        [ForeignKey("CancellationId")]
        public CancelReservation CancelReservation { get; set; }
    }
}
