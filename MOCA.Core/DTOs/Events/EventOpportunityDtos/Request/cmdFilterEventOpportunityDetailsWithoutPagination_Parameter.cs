namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Request
{
    public class cmdFilterEventOpportunityDetailsWithoutPagination_Parameter
    {
        public long? Id { get; set; } = 0;
        public DateTime? FromSubmissionDate { get; set; }
        public DateTime? ToSubmissionDate { get; set; }
        public long? Requester { get; set; }
        public long? Initiated { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public int LocationType_ID { get; set; }
    }
}
