namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Request
{
    public class cmd_Get_EventOpportunityDetails_WithPagination_Query
    {
        public cmd_Get_EventOpportunityDetails_WithPagination_Query(int pageNumber, int pageSize)
        {
            this.pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            this.pageSize = pageSize <= 0 ? 10 : pageSize;
        }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int LocationType_ID { get; set; }
    }
}
