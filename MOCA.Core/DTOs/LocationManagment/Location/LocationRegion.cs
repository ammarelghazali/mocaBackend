using MOCA.Core.DTOs.LocationManagment.City;
using MOCA.Core.DTOs.LocationManagment.Country;
using MOCA.Core.DTOs.LocationManagment.District;

namespace MOCA.Core.DTOs.LocationManagment.Location
{
    public class LocationRegion
    {
        public List<DistrictModel> Districts { get; set; }
        public List<CityModel> Cities { get; set; }
        public CountryModel Country { get; set; }
    }
}
