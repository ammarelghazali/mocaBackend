using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface ILocationCurrencyRepository : IGenericRepository<LocationCurrency>
    {
        Task<bool> DeleteByLocationID(long LocationID);
        Task<List<LocationCurrency>> GetByLocationID(long LocationID);
        Task<bool> CheckLocationCurrencyIsUinque(long LocationId, long CurrencyId);
    }
}
