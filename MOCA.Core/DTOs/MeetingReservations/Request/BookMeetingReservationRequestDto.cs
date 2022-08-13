using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MeetingReservations.Request
{
    public class BookMeetingReservationRequestDto
    {
        public DateTime DateAndTime { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Number of attendees must be greater than zero")]
        public int NumOfAttendees { get; set; }
        public long MeetingSpaceId { get; set; }
        public long LocationId { get; set; }
        public long MeetingSpaceHourlyPricingId { set; get; }
    }
}
