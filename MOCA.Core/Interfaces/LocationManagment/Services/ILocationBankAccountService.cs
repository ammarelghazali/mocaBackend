using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface ILocationBankAccountService
    {
        Task<Response<long>> AddLocationBankAccount(LocationBankAccountModel request);
        Task<Response<bool>> UpdateLocationBankAccount(LocationBankAccountModel request);
        Task<Response<LocationBankAccountModel>> GetLocationBankAccountByLocationID(long LocationID);
        Task<Response<bool>> DeleteLocationBankAccountByLocationID(long ByLocationID);
    }
}
