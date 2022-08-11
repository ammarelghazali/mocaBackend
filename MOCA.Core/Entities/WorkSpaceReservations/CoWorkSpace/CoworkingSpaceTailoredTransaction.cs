using MOCA.Core.Entities.Shared.Reservations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations.CoWorkSpace
{
    public class CoworkingSpaceTailoredTransaction
    {
        [Key, Column(Order = 1)]
        public long CoworkingSpaceReservationTailoredId { get; set; }

        [ForeignKey("CoworkingSpaceReservationTailoredId")]
        public CoworkingSpaceReservationTailored CoworkingSpaceReservationTailored { get; set; }

        [Key, Column(Order = 2)]
        public long ReservationTransactionId { get; set; }

        [ForeignKey("ReservationTransactionId")]
        public ReservationTransaction ReservationTransaction { get; set; }
    }
}
