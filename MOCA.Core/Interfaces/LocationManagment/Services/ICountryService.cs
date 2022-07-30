using MOCA.Core.DTOs.LocationManagment.Country;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;


namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface ICountryService
    {
        Task<Response<long>> AddCountry(CountryModel request);
        Task<Response<bool>> UpdateCountry(CountryModel request);
        Task<Response<CountryModel>> GetCountryByID(long Id);
        Task<PagedResponse<List<CountryModel>>> GetAllCountryWithPagination(RequestParameter filter);
        Task<Response<List<CountryModel>>> GetAllCountryWithoutPagination();
        Task<Response<bool>> DeleteCountry(long Id);
    }
}
