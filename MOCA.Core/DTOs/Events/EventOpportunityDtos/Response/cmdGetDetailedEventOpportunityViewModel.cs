namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Response
{
    public class cmdGetDetailedEventOpportunityViewModel
    {
        public OpportunityInfoViewModel OpportunityInfo { get; set; }
        public List<opportunityStageReportViewModel> opportunityStageReport { get; set; }
        public GeneralViewModel General { get; set; }
        public CompanyInfoViewModel CompanyInfo { get; set; }
        public IndividualDetailsViewModel IndividualDetails { get; set; }
        public EventDetailsViewModel EventDetails { get; set; }
    }
}
