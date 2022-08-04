using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface ILocationCurrencyService
    {
        Task<Response<bool>> AddLocationCurrencies(List<LocationCurrencyModel> request);
        Task<Response<bool>> DeleteLocationCurrencies(long LocationID);
        Task<Response<List<LocationCurrencyModel>>> GetLocationCurrenciesByLocationID(long LocationID);
    }
}
