namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Response
{
    public class EventOpportunityDetailsViewModel
    {
        public long Opportunity_ID { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string OpportunityOwner { get; set; }
        public long EventRequester_ID { get; set; }
        public string CompanyName { get; set; }
        public List<EventOpportunityContactDetailsViewModel> ContactDetails { get; set; }
        public List<SendEmailViewModel> EmailTemplate { get; set; }
    }
}
