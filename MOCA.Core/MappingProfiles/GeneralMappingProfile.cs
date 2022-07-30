using AutoMapper;
using MOCA.Core.DTOs.LocationManagment.City;
using MOCA.Core.DTOs.LocationManagment.Country;
using MOCA.Core.DTOs.LocationManagment.District;
using MOCA.Core.Entities.LocationManagment;

namespace MOCA.Core.MappingProfiles
{
    public class GeneralMappingProfile : Profile
    {
        public GeneralMappingProfile()
        {
            #region Moca Settings

            #endregion

            #region Location Managment

            CreateMap<CountryModel, Country>();
            CreateMap<Country, CountryModel>();

            CreateMap<CityModel, City>();
            CreateMap<City, CityModel>();

            CreateMap<DistrictModel, District>();
            CreateMap<District, DistrictModel>();
            #endregion

        }
    }
}
