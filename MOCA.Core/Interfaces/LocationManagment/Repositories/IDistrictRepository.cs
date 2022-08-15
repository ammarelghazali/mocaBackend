using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface IDistrictRepository : IGenericRepository<District>
    {
        Task<bool> HasAnyRelatedEntities(long DistrictId);
        Task<List<District>> GetDistrictsByCityId(long cityId);
        Task<bool> IsUniqueNameAsync(string districtName, long? id = null);
        Task<bool> IsUniqueNameAsync(string districtName);
        Task<bool> DeleteDistrict(long Id);
    }
}
