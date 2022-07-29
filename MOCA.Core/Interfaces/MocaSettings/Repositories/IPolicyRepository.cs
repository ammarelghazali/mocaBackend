using MOCA.Core.Entities.MocaSetting;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IPolicyRepository : IBaseRepository<Policy>
    {
        Task<bool> PolicyExistsAsync(long id);
        Task<bool> PolicyExistsAsync(long id, long typeId, long? lobSpaceTypeId);
        Task<IList<Policy>> GetAllPoliciesAsync(long? lobSpaceTypeId);
        Task<Policy> GetPolicyByTypeIdAsync(long typeId, long? lobSpaceTypeId);
        Task<Policy> GetPolicyByIdAndLobId(long id, long? lobSpaceTypeId);
    }
}
