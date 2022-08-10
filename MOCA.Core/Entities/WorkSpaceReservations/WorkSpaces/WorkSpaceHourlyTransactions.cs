using MOCA.Core.Entities.Shared.Reservations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces
{
    public class WorkSpaceHourlyTransactions
    {
        [Key, Column(Order = 1)]
        public long WorkSpaceReservationHourlyId { get; set; }

        [ForeignKey("WorkSpaceReservationHourlyId")]
        public WorkSpaceReservationHourly WorkSpaceReservationHourly { get; set; }

        [Key, Column(Order = 2)]
        public long ReservationTransactionId { get; set; }

        [ForeignKey("ReservationTransactionId")]
        public ReservationTransaction ReservationTransaction { get; set; }
    }
}
