namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Response
{
    public class cmd_Get_DetailedEventOpportunity_ViewModel
    {
        public OpportunityInfo_ViewModel OpportunityInfo { get; set; }
        public List<opportunityStageReport_ViewModel> opportunityStageReport { get; set; }
        public General_ViewModel General { get; set; }
        public CompanyInfo_ViewModel CompanyInfo { get; set; }
        public IndividualDetails_ViewModel IndividualDetails { get; set; }
        public EventDetails_ViewModel EventDetails { get; set; }
    }
}
