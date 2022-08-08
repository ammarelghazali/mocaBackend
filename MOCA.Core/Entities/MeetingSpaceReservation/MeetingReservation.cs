
using System.ComponentModel.DataAnnotations.Schema;
using MOCA.Core.Entities.Shared.Reservations;

namespace MOCA.Core.Entities.MeetingSpaceReservation
{
    public class MeetingReservation : BaseReservationEntity
    {

        public DateTime Date { set; get; }
        public TimeSpan Time { set; get; }
        public int NumOfAttendees { set; get; }
        public long? MeetingroomId { set; get; }
        //[ForeignKey("MeetingRoomId")]
        //public MeetingRoom MeetingRoom { get; set; }

        public long MeetingRoomTimePriceId { set; get; }
        //[ForeignKey("MeetingRoomTimePriceId")]
        //public MeetingRoomTimePrice MeetingRoomTimePrice { get; set; }
        public ICollection<MeetingReservationTopUp> MeetingReservationTopUps { get; set; }

        // public long VenueId {get; set;}
        //[ForeignKey("VenueId")]
        // public Venue Venue {get; set;}
        
        public ICollection<MeetingAttendee> MeetingAttendees { set; get; }
        public MeetingReservationTransaction MeetingReservationTransaction { get; set; }

    }
}


