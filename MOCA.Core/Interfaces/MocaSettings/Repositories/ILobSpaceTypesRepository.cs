using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface ILobSpaceTypesRepository : IGenericRepository<LocationType>
    {
        Task<LocationType> GetByName(string name);
        Task<IList<LocationType>> GetAllTypes();
        Task<bool> LobSpaceTypeExists(long id);
        Task UpdatedRelatedContent(long oldLobId, long newLobId, Guid user);
        Task DeleteRelatedContent(long lobSpaceTypeId, Guid user);
    }
}
