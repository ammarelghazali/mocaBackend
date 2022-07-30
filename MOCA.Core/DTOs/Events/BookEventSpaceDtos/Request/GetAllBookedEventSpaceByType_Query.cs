namespace MOCA.Core.DTOs.Events.BookEventSpaceDtos.Request
{
    public class GetAllBookedEventSpaceByType_Query
    {
        public GetAllBookedEventSpaceByType_Query(int pageNumber, int pageSize)
        {
            this.pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            this.pageSize = pageSize <= 0 ? 10 : pageSize;
        }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int TypeLocation { get; set; }
        public string LocationName { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
    }
}
