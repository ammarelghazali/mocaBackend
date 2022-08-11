using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Entities.SSO;
using MOCA.Core.Entities.WorkSpaceReservations.CoWorkSpace;
using MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces;
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
        public long BasicUserId { get; set; }

        [ForeignKey("BasicUserId")]
        public BasicUser BasicUser { get; set; }

        [Required]
        public long LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        [Required]
        public long ReservationTargetId { get; set; }

        [Required]
        public long RemainingHours { get; set; }

        public int? TotalHours { get; set; }

        public DateTime? ExtendExpiryDate { get; set; }

        public ICollection<ReservationDetail> ReservationDetails { get; set; }

        public CoworkingSpaceHourlyTransaction CoworkingSpaceHourlyTransaction { get; set; }
        public CoworkingSpaceBundleTransaction CoworkingSpaceBundleTransaction { get; set; }
        public CoworkingSpaceTailoredTransaction CoworkingSpaceTailoredTransaction { get; set; }

        public WorkSpaceHourlyTransaction WorkSpaceHourlyTransaction { get; set; }
        public WorkSpaceTailoredTransaction WorkSpaceTailoredTransaction { get; set; }
        public WorkSpaceBundleTransaction WorkSpaceBundleTransaction { get; set; }
    }
}   
