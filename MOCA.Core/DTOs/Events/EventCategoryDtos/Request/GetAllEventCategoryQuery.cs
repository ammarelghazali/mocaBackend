namespace MOCA.Core.DTOs.Events.EventCategoryDtos.Request
{
    public class GetAllEventCategoryQuery
    {
        public GetAllEventCategoryQuery(int pageNumber, int pageSize)
        {
            this.pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            this.pageSize = pageSize <= 0 ? 10 : pageSize;
        }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
