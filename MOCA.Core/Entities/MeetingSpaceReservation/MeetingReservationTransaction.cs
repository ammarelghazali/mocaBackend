using System.ComponentModel.DataAnnotations.Schema;
using MOCA.Core.Entities.Shared.Reservations;

namespace MOCA.Core.Entities.MeetingSpaceReservation
{
    public class MeetingReservationTransaction
    {
        public long MeetingReservationId { get; set; }
        [ForeignKey("MeetingReservationId")]
        public MeetingReservation MeetingReservation { get; set; }

        public long ReservationTransactionId { get; set; }
        [ForeignKey("ReservationTransactionId")]
        public ReservationTransaction ReservationTransaction { get; set; }
    }
}
