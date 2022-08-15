namespace MOCA.Core.DTOs.LocationManagment.Location
{
    public class RequestGetAllLocationWithoutPaginationParameter
    {
        public long? Id { get; set; }
        public long? CityId { get; set; }
        public long? DistrictId { get; set; }
        public int? ContractLength { get; set; }
        public long? LocationTypeId { get; set; }
        public DateTime? FromContractStartDate { get; set; }
        public DateTime? ToContractStartDate { get; set; }
        public DateTime? FromLaunchDate { get; set; }
        public DateTime? ToLaunchDate { get; set; }
        public decimal? FromNetArea { get; set; }
        public decimal? ToNetArea { get; set; }
    }
}
