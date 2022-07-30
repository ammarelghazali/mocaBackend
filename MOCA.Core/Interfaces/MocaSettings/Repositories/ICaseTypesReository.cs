using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface ICaseTypesReository : IGenericRepository<CaseType>, IBaseAllGetableWithoutPrarmRepository<CaseType>
    {
        Task<bool> CaseTypeNameExists(string name);
        Task<bool> CaseTypeExists(long caseTypeId);
    }
}
