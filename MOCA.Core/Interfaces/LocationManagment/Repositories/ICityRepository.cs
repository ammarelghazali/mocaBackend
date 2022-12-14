using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<bool> HasAnyDistricts(long cityId);
        Task<IReadOnlyList<City>> GetCitiesByCountryId(long countryId);
        Task<bool> IsUniqueNameAsync(string cityName, long? id = null);
        Task<bool> IsUniqueNameAsync(string cityName);
        Task<bool> DeleteCity(long Id);
    }
}
