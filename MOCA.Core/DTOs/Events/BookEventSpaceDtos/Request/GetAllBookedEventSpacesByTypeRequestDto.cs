namespace MOCA.Core.DTOs.Events.BookEventSpaceDtos.Request
{
    public class GetAllBookedEventSpacesByTypeRequestDto
    {
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }
        public string? LocationName { get; set; }
        public int TypeLocation { get; set; }

    }

}
