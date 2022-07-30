using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface ITopUpTypesRepository : IRepository<TopUpType>, IBaseAllGetableWithoutPrarmRepository<TopUpType>
    {
        Task<TopUpType> GetByName(string name);
        Task<bool> UpdateRelatedTopUps(long oldId, long newId);
        Task<bool> DeleteRelatedTopUps(long TopUpTypeId);
    }
}
