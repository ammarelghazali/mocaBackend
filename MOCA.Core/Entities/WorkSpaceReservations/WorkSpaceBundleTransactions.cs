using MOCA.Core.Entities.Shared.Reservations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations
{
    public class WorkSpaceBundleTransactions
    {
        [Key, Column(Order = 1)]
        public long WorkSpaceReservationBundleId { get; set; }

        [ForeignKey("WorkSpaceReservationBundleId")]
        public WorkSpaceReservationBundle WorkSpaceReservationBundle { get; set; }

        [Key, Column(Order = 2)]
        public long ReservationTransactionId { get; set; }

        [ForeignKey("ReservationTransactionId")]
        public ReservationTransaction ReservationTransaction { get; set; }
    }
}
