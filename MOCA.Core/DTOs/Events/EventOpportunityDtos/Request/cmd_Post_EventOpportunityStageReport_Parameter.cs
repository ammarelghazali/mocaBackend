namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Request
{
    public class cmd_Post_EventOpportunityStageReport_Parameter
    {
        public long Opportunity_ID { get; set; }
        public long OpportunityStage_ID { get; set; }
        public string? OpportunityUpdate { get; set; }
        public DateTime? Reminder { get; set; }
    }
}
