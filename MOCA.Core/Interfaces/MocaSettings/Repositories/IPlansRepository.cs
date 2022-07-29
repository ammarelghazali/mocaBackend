using MOCA.Core.Entities.MocaSetting;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IPlansRepository : IBaseRepository<Plan>, IBaseAllGetableRepository<Plan>
    {
        Task<Plan> GetByType(long lobSpaceTypeId, long typeId);
        Task<Plan> GetByType(long typeId);
        Task<IList<Plan>> GetAllPlansByTypeId(long planTypeId);
    }
}
