using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.Shared.Reservations
{
    public class ReservationTransaction : BaseEntity
    {
        [Required]
        public long ReservationTypeId { get; set; }

        [ForeignKey("ReservationTypeId")]
        public ReservationType ReservationType { get; set; }

        // TODO: Column to determinte the plan days for workspace or multiple ids for each type

        [Required]
        public long ReservationTargetId { get; set; }

        [Required]
        public long RemainingHours { get; set; }

        public int? TotalHours { get; set; }

        public DateTime? ExtendExpiryDate { get; set; }
    }
}
