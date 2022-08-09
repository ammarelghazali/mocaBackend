using System.ComponentModel.DataAnnotations.Schema;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Entities.Shared.Reservations;

namespace MOCA.Core.Entities.MeetingSpaceReservation
{
    public class MeetingReservation : BaseReservationEntity
    {
        public DateTime Date { set; get; }
        public TimeSpan Time { set; get; }
        public int NumOfAttendees { set; get; }
        public long MeetingSpaceId { set; get; }
        [ForeignKey("MeetingSpaceId")]
        public MeetingSpace MeetingSpace { get; set; }

        public long MeetingSpaceHourlyPricingId { set; get; }
        [ForeignKey("MeetingSpaceHourlyPricingId")]
        public MeetingSpaceHourlyPricing MeetingSpaceHourlyPricing { get; set; }

        public ICollection<MeetingAttendee> MeetingAttendees { set; get; }
        public ICollection<MeetingReservationTopUp> MeetingReservationTopUps { get; set; }
        public MeetingReservationTransaction MeetingReservationTransaction { get; set; }
        public MeetingReservationCancellation MeetingReservationCancellation { get; set; }

    }
}


