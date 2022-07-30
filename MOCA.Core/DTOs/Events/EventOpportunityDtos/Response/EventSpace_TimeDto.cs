namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Response
{
    public class EventSpace_TimeDto
    {
        public long BookEventSpace_ID { get; set; }
        public DateTime? RecurrenceStartDate { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }
        public string? RecurrenceStartTime { get; set; }
        public string? RecurrenceEndTime { get; set; }
        public string? RecurrenceDay { get; set; }
    }
}
