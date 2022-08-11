using MOCA.Core.Entities.Shared.Reservations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces
{
    public class WorkSpaceBundleCancellation
    {
        [Key, Column(Order = 1)]
        public long WorkSpaceBundleReservationId { get; set; }

        [ForeignKey("WorkSpaceBundleReservationId")]
        public WorkSpaceReservationBundle WorkSpaceReservationBundle { get; set; }

        [Key, Column(Order = 2)]
        public long CancellationId { get; set; }

        [ForeignKey("CancellationId")]
        public CancelReservation CancelReservation { get; set; }
    }
}
