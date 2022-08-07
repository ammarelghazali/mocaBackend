namespace MOCA.Core.DTOs.LocationManagment.Building.FilterParameter
{
    public class GetWithoutPaginatedBuildingFilterParameter
    {
        public long? Id { get; set; }
        public string Building { get; set; }
        public int? FromGross { get; set; }
        public int? ToGross { get; set; }
        public int? FromNet { get; set; }
        public int? ToNet { get; set; }
        public int? FromTotalFloors { get; set; }
        public int? ToTotalFloors { get; set; }
    }
}
