using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.MeetingSpaceReservation;
using System.ComponentModel.DataAnnotations;
using MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces;

namespace MOCA.Core.Entities.Shared.Reservations
{
    public class PaymentMethod : BaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        public ICollection<WorkSpaceReservationHourly> WorkSpaceHourlyReservations { get; set; }
        public ICollection<WorkSpaceReservationTailored> WorkSpaceTailoredReservations { get; set; }
        public ICollection<WorkSpaceReservationBundle> WorkSpaceBundleReservations { get; set; }
        public ICollection<WorkSpaceHourlyTopUp> WorkSpaceHourlyTopUps { get; set; }
        public ICollection<WorkSpaceTailoredTopUp> WorkSpaceTailoredTopUps { get; set; }

        public ICollection<MeetingReservation> MeetingSpaceReservations { get; set; }
        public ICollection<MeetingReservationTopUp> MeetingReservationTopUps { get; set; }

    }
}
