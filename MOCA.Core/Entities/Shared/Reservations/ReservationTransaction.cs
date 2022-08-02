using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.SSO;
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

        [Required]
        public string BasicUserId { get; set; }

        [ForeignKey("BasicUserId")]
        public BasicUser BasicUser { get; set; }

        [Required]
        public long ReservationTargetId { get; set; }

        [Required]
        public long RemainingHours { get; set; }

        public int? TotalHours { get; set; }

        public DateTime? ExtendExpiryDate { get; set; }

        public ICollection<ReservationDetail> ReservationDetails { get; set; }
    }
}   
