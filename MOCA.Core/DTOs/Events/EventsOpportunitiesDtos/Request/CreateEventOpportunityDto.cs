namespace MOCA.Core.DTOs.Events.EventsOpportunitiesDtos.Request
{
    public class CreateEventOpportunityDto
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
        public string ImagePath { get; set; }
        public int EmailTemplateTypeID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
