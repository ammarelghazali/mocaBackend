using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface ICurrencyRepository : IGenericRepository<Currency>
    {
        Task<Currency> CurrencyIsExists(long CurrencyID);
        Task<bool> HasAnyRelatedEntities(long CurrencyID);
        Task<bool> DeleteCurrency(long Id);
    }
}
