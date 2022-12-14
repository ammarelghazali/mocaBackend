using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IWifisRepository : IGenericRepository<Wifi>
    {
        Task<Wifi> GetWifiByLobSpaceTypeId(long? lobSpaceTypeId);
    }
}
