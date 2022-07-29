using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface ITopUpsRespository : IRepository<TopUp>, IBaseAllGetableWithoutPrarmRepository<TopUp>
    {
        Task<TopUp> GetByTopUpTypeId(long topUpTypeId);
        Task<TopUp> GetByTopUpTypeId(long topUpTypeId, long lobSpaceTypeId);
        Task<List<TopUp>> GetAllByType(long id);
    }
}
