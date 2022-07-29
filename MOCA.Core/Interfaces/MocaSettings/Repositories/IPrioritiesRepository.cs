using MOCA.Core.Entities.MocaSetting;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IPrioritiesRepository : IBaseRepository<Priority>, IBaseAllGetableWithoutPrarmRepository<Priority>
    {
        Task<bool> PriorityNameExists(string name);
        Task<bool> PriorityExists(long priorityId);
    }
}
