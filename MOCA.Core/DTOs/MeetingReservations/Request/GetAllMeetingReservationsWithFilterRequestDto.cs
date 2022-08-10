namespace MOCA.Core.DTOs.MeetingReservations.Request
{
    public class GetAllMeetingReservationsWithFilterRequestDto
    {
        public long? Id { get; set; }
        public string? LocationName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long? FromHours { get; set; }
        public long? ToHours { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? ToTime { get; set; }
        public DateTime? FromScanTime { get; set; }
        public DateTime? ToScanTime { get; set; }
        public int? FromNumOfAttendees { get; set; }
        public int? ToNumOfAttendees { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public string? MobileNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int PageSize { get; set; }
        public long? MeetingSpaceId { set; get; }
        public string? Status { get; set; }
        public string? VenueName { get; set; }
    }
}
