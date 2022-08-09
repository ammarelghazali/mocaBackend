using MOCA.Core.Entities.MeetingSpaceReservation;

namespace MOCA.Core.DTOs.MeetingReservations.Response
{
    public class MeetingReservationResponseDto
    {
        public long Id { get; set; }
        public DateTime SubmissionDate { get; set; }
        public long UserId { get; set; }
        public long LocationId { get; set; }
        public string LocationName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string VenueName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime? ScanInDate { get; set; }
        public DateTime? ScanOutDate { get; set; }
        public int Hourse { get; set; }
        public int NumOfGuests { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public bool HasTopUp { get; set; }
        public decimal RemainingHours { get; set; }

        public ICollection<MeetingReservationTopUp> TopUps { get; set; }
        public ICollection<MeetingAttendee> Attendees { get; set; }


    }
}

