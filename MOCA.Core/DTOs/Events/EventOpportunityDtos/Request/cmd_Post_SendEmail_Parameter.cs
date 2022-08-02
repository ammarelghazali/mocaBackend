namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Request
{
    public class cmd_Post_SendEmail_Parameter
    {
        public long EventsOpportunitiesId { get; set; }
        public IList<string> ToUsers { get; set; }
        public string? CC { get; set; }
        public string? Subject { get; set; }
        public string Body { get; set; }
        public int IsUser { get; set; }
        public string? userName { get; set; }
        public string? password { get; set; }
    }

}
