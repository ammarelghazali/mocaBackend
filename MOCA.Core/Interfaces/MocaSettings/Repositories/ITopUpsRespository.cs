using MOCA.Core.Entities.MocaSetting;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface ITopUpsRespository : IBaseRepository<TopUp>, IBaseAllGetableWithoutPrarmRepository<TopUp>
    {
        Task<TopUp> GetByTopUpTypeId(long topUpTypeId);
        Task<TopUp> GetByTopUpTypeId(long topUpTypeId, long lobSpaceTypeId);
        Task<List<TopUp>> GetAllByType(long id);
    }
}
