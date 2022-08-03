namespace MOCA.Core.DTOs.Events.EventsOpportunitiesDtos.Response
{
    public class GetEmailTempleteEventOpportunitylViewModelDto
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ImagePath { get; set; }
        public int EmailTemplateTypeID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
