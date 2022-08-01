using MOCA.Core.DTOs.LocationManagment.City;
using MOCA.Core.DTOs.LocationManagment.District;
using MOCA.Core.DTOs.Shared;

namespace MOCA.Core.DTOs.LocationManagment.Location
{
    public class LocationDropDown
    {
        public List<DistrictModel> Districts { get; set; }
        public List<CityModel> Cities { get; set; }
        public List<DropdownViewModel> Locations { get; set; }
    }
}
