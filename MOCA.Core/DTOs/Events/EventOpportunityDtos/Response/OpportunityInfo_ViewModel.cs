namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Response
{
    public class OpportunityInfo_ViewModel
    {
        public long OpportunityID { get; set; }
        public string LOS { get; set; }
        public string SubmissionDate { get; set; }
        public string OpportunityOwner { get; set; }
        public string MembershipStatus { get; set; }
        public List<OpportunitySatgeModel_ViewModel> OpportunityStage { get; set; }
    }

}
