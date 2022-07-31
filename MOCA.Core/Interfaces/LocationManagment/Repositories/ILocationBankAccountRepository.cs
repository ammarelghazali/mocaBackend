using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface ILocationBankAccountRepository : IGenericRepository<LocationBankAccount>
    {
        Task<bool> DeleteByLocationID(long LocationID);
        Task<LocationBankAccount> GetByLocationID(long LocationID);
    }
}
