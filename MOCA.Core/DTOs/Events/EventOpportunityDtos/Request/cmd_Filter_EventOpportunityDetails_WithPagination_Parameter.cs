namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Request
{
    public class cmd_Filter_EventOpportunityDetails_WithPagination_Parameter
    {
        public long? Id { get; set; }
        public DateTime? FromSubmissionDate { get; set; }
        public DateTime? ToSubmissionDate { get; set; }
        public long? Requester { get; set; }
        public long? Initiated { get; set; }
        public string? Name { get; set; }
        public string? OwnerName { get; set; }
        public int? pageNumber { get; set; } = 1;
        public int? pageSize { get; set; } = 10;
        public int? LocationType_ID { get; set; }

    }
}
