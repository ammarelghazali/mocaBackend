namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Request
{
    public class cmdPostEventOpportunityStageReportParameter
    {
        public long OpportunityId { get; set; }
        public long OpportunityStageId { get; set; }
        public string? OpportunityUpdate { get; set; }
        public DateTime? Reminder { get; set; }
    }
}
