namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Request
{
    public class cmdFilterEventOpportunityDetailsWithPagination_Query
    {
        public cmdFilterEventOpportunityDetailsWithPagination_Query(int pageNumber, int pageSize)
        {
            this.pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            this.pageSize = pageSize <= 0 ? 10 : pageSize;
        }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public long LocationTypeId { get; set; }
        public long? Id { get; set; }
        public DateTime? FromSubmissionDate { get; set; }
        public DateTime? ToSubmissionDate { get; set; }
        public long? Requester { get; set; }
        public long? Initiated { get; set; }
        public string? Name { get; set; }
        public string? OwnerName { get; set; }
    }
}
