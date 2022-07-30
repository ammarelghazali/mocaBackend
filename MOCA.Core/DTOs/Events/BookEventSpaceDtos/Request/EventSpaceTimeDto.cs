namespace MOCA.Core.DTOs.Events.BookEventSpaceDtos.Request
{
    public class EventSpaceTimeDto
    {
        public long? Id { get; set; }
        public long? BookEventSpace_ID { get; set; }
        public DateTime RecurrenceStartDate { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }
        public DateTime RecurrenceStartTime { get; set; }
        public DateTime RecurrenceEndTime { get; set; }
        public string? RecurrenceDay { get; set; }
    }
}
