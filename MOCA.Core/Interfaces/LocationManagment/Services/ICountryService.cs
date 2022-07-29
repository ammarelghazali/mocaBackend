using MOCA.Core.DTOs.LocationManagment;
using MOCA.Core.DTOs.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface ICountryService
    {
        Task<Response<long>> AddCountry(CountryModel request);
    }
}
