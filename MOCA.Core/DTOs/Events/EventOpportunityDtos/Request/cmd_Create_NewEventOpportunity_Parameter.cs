
namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Request
{
    public class cmd_Create_NewEventOpportunity_Parameter
    {
        public long? EventRequesterId { get; set; }
        public long InitiatedId { get; set; }
        public string? CompanyName { get; set; }
        public long? LobLocationTypeId { get; set; }
        public List<EventOpportunityContactDetail_ViewModel> ContactDetails { get; set; }
    }
}
