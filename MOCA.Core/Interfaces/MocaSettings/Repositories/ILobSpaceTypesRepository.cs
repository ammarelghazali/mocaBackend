using MOCA.Core.Entities.LocationManagment;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface ILobSpaceTypesRepository : IBaseRepository<LocationType>
    {
        Task<LocationType> GetByName(string name);
        Task<IList<LocationType>> GetAllTypes();
        Task<bool> LobSpaceTypeExists(long id);
        Task UpdatedRelatedContent(long oldLobId, long newLobId, Guid user);
        Task DeleteRelatedContent(long lobSpaceTypeId, Guid user);
    }
}
