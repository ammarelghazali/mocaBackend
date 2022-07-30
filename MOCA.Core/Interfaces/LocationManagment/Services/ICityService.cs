using Compolitan.Core.DTOs;
using MOCA.Core.DTOs.LocationManagment.City;
using MOCA.Core.DTOs.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface ICityService
    {
        Task<Response<long>> AddCity(CityModel request);
        Task<Response<bool>> UpdateCity(CityModel request);
        Task<Response<CityModel>> GetCityByID(long Id);
        Task<PagedResponse<List<CityModel>>> GetAllCityWithPagination(RequestParameter filter);
        Task<Response<List<CityModel>>> GetAllCityWithoutPagination();
        Task<Response<List<CityModel>>> GetAllCityByCountryID(long CountryID);
        Task<Response<bool>> DeleteCity(long Id);
    }
}
