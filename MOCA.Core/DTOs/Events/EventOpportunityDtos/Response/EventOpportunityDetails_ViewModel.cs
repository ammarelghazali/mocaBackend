namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Response
{
    public class EventOpportunityDetails_ViewModel
    {
        public long Opportunity_ID { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string OpportunityOwner { get; set; }
        public long EventRequester_ID { get; set; }
        public string CompanyName { get; set; }
        public List<EventOpportunityContactDetails_ViewModel> ContactDetails { get; set; }
        public List<SendEmail_ViewModel> EmailTemplate { get; set; }
    }
}
