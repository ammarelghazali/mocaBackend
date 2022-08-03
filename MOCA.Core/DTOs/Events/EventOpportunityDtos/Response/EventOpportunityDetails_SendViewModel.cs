
using MOCA.Core.Entities.EventSpaceBookings;

namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Response
{
    public class EventOpportunityDetails_SendViewModel
    {
        public int ID { get; set; }
        public long Opportunity_ID { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string OpportunityOwner { get; set; }
        public Initiated Initiated { get; set; }
        public long EventRequester_ID { get; set; }
        public string EventRequester_Name { get; set; }
        public string CompanyName { get; set; }
        public List<EventOpportunityContactDetailsViewModel> ContactDetails { get; set; }
        public List<SendEmail> SendEmails { get; set; }
        public int pg_total { get; set; }

    }
}
