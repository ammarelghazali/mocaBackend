namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Request
{
    public class cmd_Get_EventOpportunityDetails_WithPagination_Parameter
    {
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public int LocationType_ID { get; set; }

    }
}
