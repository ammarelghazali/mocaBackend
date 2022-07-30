using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface IInclusionRepository : IGenericRepository<Inclusion>
    {
        Task<bool> DeleteInclusion(long Id);
        Task<bool> HasAnyRelatedEntities(long InclusionID);
    }
}
