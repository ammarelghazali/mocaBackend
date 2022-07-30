using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface ICityRepository : IRepository<City>
    {
        Task<bool> HasAnyDistricts(long cityId);
        Task<IReadOnlyList<City>> GetCitiesByCountryId(long countryId);
        Task<bool> DeleteCity(long Id);
    }
}
