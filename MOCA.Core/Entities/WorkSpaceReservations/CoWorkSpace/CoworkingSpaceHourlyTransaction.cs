using MOCA.Core.Entities.Shared.Reservations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations.CoWorkSpace
{
    public class CoworkingSpaceHourlyTransaction
    {
        [Key, Column(Order = 1)]
        public long CoworkingSpaceReservationHourlyId { get; set; }

        [ForeignKey("CoworkingSpaceReservationHourlyId")]
        public CoworkingSpaceReservationHourly CoworkingSpaceReservationHourly { get; set; }

        [Key, Column(Order = 2)]
        public long ReservationTransactionId { get; set; }

        [ForeignKey("ReservationTransactionId")]
        public ReservationTransaction ReservationTransaction { get; set; }
    }
}
