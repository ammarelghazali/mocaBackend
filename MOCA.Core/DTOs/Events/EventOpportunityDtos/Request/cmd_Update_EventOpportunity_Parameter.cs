
namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Request
{
    public class cmd_Update_EventOpportunity_Parameter
    {
        public long Opportunity_ID { get; set; }
        public long EventRequester_ID { get; set; }
        public string? CompanyName { get; set; }
        public int LocationType_ID { get; set; }
        public List<EventOpportunityContactDetail_ViewModel> ContactDetails { get; set; }
    }
}
