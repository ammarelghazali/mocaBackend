namespace MOCA.Core.DTOs.Events.EventsOpportunitiesDtos.Request
{
    public class CreateEventOpportunityParameterDto
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ImagePath { get; set; }
        public int EmailTemplateTypeID { get; set; }
    }
}
