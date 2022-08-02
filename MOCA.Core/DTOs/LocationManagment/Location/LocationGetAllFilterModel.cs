namespace MOCA.Core.DTOs.LocationManagment.Location
{
    public class LocationGetAllFilterModel
    {
        public long Id { get; set; }
        public string LocationName { get; set; }
        public long LocationTypeId { get; set; }
        public string LocationTypeName { get; set; }
        public long DistrictId { get; set; }
        public string DistrictName { get; set; }
        public long CityId { get; set; }
        public string CityName { get; set; }
        public decimal GrossArea { get; set; }
        public decimal NetArea { get; set; }
        public DateTime ContractStartDate { get; set; }
        public int ContractLength { get; set; }
        public bool IsPublish { get; set; }
        public long pgTotal { get; set; }
    }
}
