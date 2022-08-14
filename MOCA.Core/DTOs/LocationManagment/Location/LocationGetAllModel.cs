using MOCA.Core.DTOs.Shared;

namespace MOCA.Core.DTOs.LocationManagment.Location
{
    public class LocationGetAllModel
    {
        public long Id { get; set; }
        public DropdownViewModel LocationType { get; set; }
        public string Name { get; set; }
        public DropdownViewModel District { get; set; }
        public DropdownViewModel City { get; set; }
        public decimal GrossArea { get; set; }
        public decimal NetArea { get; set; }
        public DateTime LaunchDate { get; set; }
        public DateTime ContractStartDate { get; set; }
        public int ContractLength { get; set; }
        public bool IsPublish { get; set; }
        public bool InstallAccessPoint { get; set; }
    }
}
