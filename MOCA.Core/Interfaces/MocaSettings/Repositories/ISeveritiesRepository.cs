using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface ISeveritiesRepository : IRepository<Severity>, IBaseAllGetableWithoutPrarmRepository<Severity>
    {
        Task<bool> SeverityExists(long severityId);
        Task<bool> SeverityExists(string SeverityName);
    }
}
