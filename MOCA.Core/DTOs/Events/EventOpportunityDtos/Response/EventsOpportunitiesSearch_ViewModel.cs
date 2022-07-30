namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Response
{
    public class EventsOpportunitiesSearch_ViewModel
    {
        public long ID { get; set; }
        public long pg_total { get; set; }
        public long? EventRequester_ID { get; set; }
        public long? Initiated_ID { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string CompanyName { get; set; }

    }
}
