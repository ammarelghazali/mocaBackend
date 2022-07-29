using MOCA.Core.Entities.MocaSetting;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IWifisRepository : IBaseRepository<Wifi>
    {
        Task<Wifi> GetWifiByLobSpaceTypeId(long? lobSpaceTypeId);
    }
}
