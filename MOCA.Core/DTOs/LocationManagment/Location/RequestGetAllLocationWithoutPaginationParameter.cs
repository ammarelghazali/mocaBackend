namespace MOCA.Core.DTOs.LocationManagment.Location
{
    public class RequestGetAllLocationWithoutPaginationParameter
    {
        public long Id { get; set; }
        public long CityId { get; set; }
        public long DistrictId { get; set; }
        public int ContractLength { get; set; }
    }
}
