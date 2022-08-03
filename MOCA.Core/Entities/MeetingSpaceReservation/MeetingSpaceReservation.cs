
using MOCA.Core.Entities.Shared.Reservations;

namespace MOCA.Core.Entities.MeetingSpaceReservation
{
    public class MeetingSpaceReservation : BaseReservationEntity
    {
        public DateTime? DatetoStart { set; get; }
        public TimeSpan? TimetoStart { set; get; }
        public int NumOfAttendees { set; get; }
        public int Hours { set; get; }
        public decimal TotalPrice { set; get; }
        public long? MeetingroomId { set; get; }
    }
}
