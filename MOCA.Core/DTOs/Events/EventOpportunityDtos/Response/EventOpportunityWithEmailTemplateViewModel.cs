using MOCA.Core.Entities.EventSpaceBookings;

namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Response
{
    public class EventOpportunityWithEmailTemplateViewModel
    {
        public long Opportunity_ID { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string OpportunityOwner { get; set; }
        public long EventRequester_ID { get; set; }
        public string CompanyName { get; set; }
        public List<EventOpportunityContactDetailsViewModel> ContactDetails { get; set; }
        public EmailTemplate EmailTemplate { get; set; }
    }
}
