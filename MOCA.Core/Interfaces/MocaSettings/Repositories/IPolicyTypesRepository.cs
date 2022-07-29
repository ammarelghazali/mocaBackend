using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IPolicyTypesRepository : IRepository<PolicyType>

    {
        Task<bool> PolicyTypeExistsAsync(long id);
        Task<bool> PolicyTypeExistsAsync(string name);
        Task<bool> PolicyTypeExistsAsync(string name, long id);
        Task<PolicyType> GetPolicyTypeByNameWithPolicyAsync(string name);
        Task<PolicyType> GetPolicyTypeByIdWithPolicyAsync(long id);
        Task<IList<PolicyType>> GetAllPolicyTypesWithPolicyDescriptionAsync(long? LobSpaceTypeId);
        Task<IList<PolicyType>> GetAllAsync();
        Task<bool> UpdateRelatedPolicy(long oldId, long newId, Guid user);
        Task DeleteRelatedPolicy(long id, Guid user);
    }
}
