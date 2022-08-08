namespace MOCA.Core.DTOs.LocationManagment.Building.FilterParameter
{
    public class GetPaginatedBuildingFilterParameter
    {
        public long? Id { get; set; }
        public string Building { get; set; }
        public int? FromGross { get; set; }
        public int? ToGross { get; set; }
        public int? FromNet { get; set; }
        public int? ToNet { get; set; }
        public int? FromTotalFloors { get; set; }
        public int? ToTotalFloors { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetPaginatedBuildingFilterParameter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public GetPaginatedBuildingFilterParameter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
