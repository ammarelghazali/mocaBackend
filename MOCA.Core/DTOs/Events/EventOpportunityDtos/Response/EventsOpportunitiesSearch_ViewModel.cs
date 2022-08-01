namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Response
{
    public class EventsOpportunitiesSearch_ViewModel
    {
        public long Id { get; set; }
        public long pg_total { get; set; }
        public long? EventRequesterId { get; set; }
        public long InitiatedId { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string CompanyName { get; set; }

    }
}
