using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface ILocationInclusionRepository : IGenericRepository<LocationInclusion>
    {
        Task<bool> DeleteAllLocationInclusionByLocationID(long LocationID);
        Task<List<LocationInclusion>> GetAllLocationInclusionByLocationID(long LocationID);
    }
}
