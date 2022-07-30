using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IPlansRepository : IRepository<Plan>, IBaseAllGetableRepository<Plan>
    {
        Task<Plan> GetByType(long lobSpaceTypeId, long typeId);
        Task<Plan> GetByType(long typeId);
        Task<List<Plan>> GetAllPlansByTypeId(long planTypeId);
    }
}
