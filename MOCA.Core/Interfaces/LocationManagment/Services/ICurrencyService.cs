using MOCA.Core.DTOs.LocationManagment.Currency;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface ICurrencyService
    {
        Task<Response<long>> AddCurrency(CurrencyModel request);
        Task<Response<bool>> UpdateCurrency(CurrencyModel request);
        Task<Response<CurrencyModel>> GetCurrencyByID(long Id);
        Task<PagedResponse<List<CurrencyModel>>> GetAllCurrenciesWithPagination(RequestParameter filter);
        Task<Response<List<CurrencyModel>>> GetAllCurrenciesWithoutPagination();
        Task<Response<bool>> DeleteCurrency(long Id);
    }
}
