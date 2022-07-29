using MOCA.Core.Entities.MocaSetting;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface ICaseTypesReository : IBaseRepository<CaseType>, IBaseAllGetableWithoutPrarmRepository<CaseType>
    {
        Task<bool> CaseTypeNameExists(string name);
        Task<bool> CaseTypeExists(long caseTypeId);
    }
}
