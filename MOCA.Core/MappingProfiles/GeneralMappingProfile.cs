using AutoMapper;
using MOCA.Core.DTOs.LocationManagment;
using MOCA.Core.Entities.LocationManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            #endregion

        }
    }
}
