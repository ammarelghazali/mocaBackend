
using System.ComponentModel.DataAnnotations.Schema;
using MOCA.Core.Entities.Shared.Reservations;

namespace MOCA.Core.Entities.MeetingSpaceReservation
{
    public class MeetingSpaceReservation : BaseReservationEntity
    {
        public long? MeetingroomId { set; get; }
        //[ForeignKey("MeetingRoomId")]
        //public MeetingRoom MeetingRoom { get; set; }

        public long MeetingRoomTimePriceId { set; get; }
        //[ForeignKey("MeetingRoomTimePriceId")]
        //public MeetingRoomTimePrice MeetingRoomTimePrice { get; set; }
        public long MeetingReservationTopUpId { get; set; }
        [ForeignKey("MeetingReservationTopUpId")]
        public MeetingReservationTopUp MeetingReservationTopUp { get; set; }

        public DateTime DatetoStart { set; get; }
        public TimeSpan TimetoStart { set; get; }
        public int NumOfAttendees { set; get; }
        // Venue
    }
}


