using MOCA.Core.Entities.MocaSetting;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface ISeveritiesRepository : IBaseRepository<Severity>, IBaseAllGetableWithoutPrarmRepository<Severity>
    {
        Task<bool> SeverityExists(long severityId);
        Task<bool> SeverityExists(string SeverityName);
    }
}
