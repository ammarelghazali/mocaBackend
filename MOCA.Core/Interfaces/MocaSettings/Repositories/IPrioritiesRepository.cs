using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IPrioritiesRepository : IGenericRepository<Priority>, IBaseAllGetableWithoutPrarmRepository<Priority>
    {
        Task<bool> PriorityNameExists(string name);
        Task<bool> PriorityExists(long priorityId);
    }
}
