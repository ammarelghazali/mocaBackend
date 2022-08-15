namespace MOCA.Core.DTOs.LocationManagment.Location
{
    public class RequestGetAllLocationParameter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long? Id { get; set; }
        public long? CityId { get; set; }
        public long? DistrictId { get; set; }
        public int? ContractLength { get; set; }
        public long? LocationTypeId { get; set; }
        public DateTime? FromContractStartDate { get; set; }
        public DateTime? ToContractStartDate { get; set; }
        public DateTime? FromLaunchDate { get; set; }
        public DateTime? ToLaunchDate { get; set; }
        public RequestGetAllLocationParameter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public RequestGetAllLocationParameter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
