using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MOCA.Core.Entities.Shared.Reservations;

namespace MOCA.Core.Entities.MeetingSpaceReservation
{
    public class MeetingReservationTransaction
    {
        [Key, Column(Order = 1)]
        public long MeetingReservationId { get; set; }
        [ForeignKey("MeetingReservationId")]
        public MeetingReservation MeetingReservation { get; set; }

        [Key, Column(Order = 2)]
        public long ReservationTransactionId { get; set; }
        [ForeignKey("ReservationTransactionId")]
        public ReservationTransaction ReservationTransaction { get; set; }
    }
}
