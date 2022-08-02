
namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Request
{
    public class cmdUpdateEventOpportunityParameter
    {
        public long OpportunityId { get; set; }
        public long EventRequesterId { get; set; }
        public string? CompanyName { get; set; }
        public int LocationTypeId { get; set; }
        public List<EventOpportunityContactDetailViewModel> ContactDetails { get; set; }
    }
}
