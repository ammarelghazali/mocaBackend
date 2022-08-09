namespace MOCA.Core.DTOs.MeetingReservations.Request
{
    public class BookMeetingReservationRequestDto
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public int NumOfAttendees { get; set; }
        public long MeetingSpaceId { get; set; }
        public long LocationId { get; set; }//
        public long MeetingSpaceHourlyPricingId { set; get; }
        public long? BasicUserId { get; set; }
    }
}
