
namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Request
{
    public class cmdCreateNewEventOpportunityParameter
    {
        public long? EventRequesterId { get; set; }
        public long InitiatedId { get; set; }
        public string? CompanyName { get; set; }
        public long? LobLocationTypeId { get; set; }
        public List<EventOpportunityContactDetailViewModel> ContactDetails { get; set; }
    }
}
