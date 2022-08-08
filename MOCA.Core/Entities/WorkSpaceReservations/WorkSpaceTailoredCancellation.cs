using MOCA.Core.Entities.Shared.Reservations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations
{
    public class WorkSpaceTailoredCancellation
    {
        [Key, Column(Order = 1)]
        public long WorkSpaceTailoredReservationId { get; set; }

        [ForeignKey("WorkSpaceTailoredReservationId")]
        public WorkSpaceReservationTailored WorkSpaceReservationTailored { get; set; }

        [Key, Column(Order = 2)]
        public long CancellationId { get; set; }

        [ForeignKey("CancellationId")]
        public CancelReservation CancelReservation { get; set; }
    }
}
