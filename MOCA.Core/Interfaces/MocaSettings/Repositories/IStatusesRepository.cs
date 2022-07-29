using MOCA.Core.Entities.MocaSetting;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IStatusesRepository : IBaseRepository<Status>, IBaseAllGetableWithoutPrarmRepository<Status>
    {
        Task<bool> StatusExists(long statusId);
        Task<bool> StatusExists(string statusName);
    }
}
