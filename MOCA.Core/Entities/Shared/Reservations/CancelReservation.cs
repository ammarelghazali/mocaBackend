using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.SSO;
using MOCA.Core.Entities.SSO.Identity;
using MOCA.Core.Entities.WorkSpaceReservations.CoWorkSpace;
using MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.Shared.Reservations
{
    public class CancelReservation : BaseEntity
    {
        [Required]
        public long ReservationTargetId { get; set; }

        /// <summary>
        /// Whether if it is Workspace, meetingrooms, or Bizlounge
        /// </summary>
        [Required]
        public long ReservationTypeId { get; set; }

        [ForeignKey("ReservationTypeId")]
        public ReservationType ReservationType { get; set; }

        /// <summary>
        /// Refund Reservation Type Enum
        /// </summary>
        [Required]
        public int RefundReservationType { get; set; }

        [Required]
        public DateTime CancelDate { get; set; }

        [Required]
        public long BasicUserId { get; set; }

        [ForeignKey("BasicUserId")]
        public BasicUser BasicUser{ get; set; }

        public string? AdminId { get; set; }

        [ForeignKey("AdminId")]
        public Admin Admin { get; set; }    

        public CoworkingSpaceHourlyCancellation CoworkingSpaceHourlyCancellation { get; set; }
        public CoworkingSpaceBundleCancellation CoworkingSpaceBundleCancellation { get; set; }
        public CoworkingSpaceTailoredCancellation CoworkingSpaceTailoredCancellation { get; set; }  

        public WorkSpaceHourlyCancellation WorkSpaceHourlyCancellation { get; set; }
        public WorkSpaceBundleCancellation WorkSpaceBundleCancellation { get; set; }
        public WorkSpaceTailoredCancellation WorkSpaceTailoredCancellation { get; set; }
    }
}
