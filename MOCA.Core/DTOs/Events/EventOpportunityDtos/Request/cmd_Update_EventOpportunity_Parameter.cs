
namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Request
{
    public class cmd_Update_EventOpportunity_Parameter
    {
        public long OpportunityId { get; set; }
        public long EventRequesterId { get; set; }
        public string? CompanyName { get; set; }
        public int LocationTypeId { get; set; }
        public List<EventOpportunityContactDetail_ViewModel> ContactDetails { get; set; }
    }
}
