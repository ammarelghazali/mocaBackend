using MOCA.Core.Entities.Shared.Reservations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces
{
    public class WorkSpaceTailoredTransactions
    {
        [Key, Column(Order = 1)]
        public long WorkSpaceReservationTailoredId { get; set; }

        [ForeignKey("WorkSpaceReservationTailoredId")]
        public WorkSpaceReservationTailored WorkSpaceReservationTailored { get; set; }

        [Key, Column(Order = 2)]
        public long ReservationTransactionId { get; set; }

        [ForeignKey("ReservationTransactionId")]
        public ReservationTransaction ReservationTransaction { get; set; }
    }
}
