using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.Shared.Reservations
{
    public class ReservationDetail : BaseEntity
    {
        [Required]
        public long ReservationTransactionId { get; set; }

        [ForeignKey("ReservationTransactionId")]
        public ReservationTransaction ReservationTransaction { get; set; }

        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
    }
}
