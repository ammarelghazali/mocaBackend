using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IStatusesRepository : IRepository<Status>, IBaseAllGetableWithoutPrarmRepository<Status>
    {
        Task<bool> StatusExists(long statusId);
        Task<bool> StatusExists(string statusName);
    }
}
