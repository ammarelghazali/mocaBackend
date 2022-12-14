using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<Country> CountryIsExists(long countryID);
        Task<bool> HasAnyCities(long CountryID);
        Task<bool> IsUniqueNameAsync(string countryName, long? id = null);
        Task<bool> IsUniqueNameAsync(string countryName);
        Task<bool> DeleteCountry(long Id);
    }
}
