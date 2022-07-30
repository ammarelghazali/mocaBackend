namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Response
{

    public class SendEmail_ViewModel
    {
        public long Id { get; set; }
        public string CC { get; set; }
        public string FromUser { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public long ContactDetails_ID { get; set; }
        public long? BookATour_ID { get; set; }
        public long? EventsOpportunities_ID { get; set; }
        public long? EmailTemplate_ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<EventOpportunityContactDetails_ViewModel> ContactDetails { get; set; }
    }
}
