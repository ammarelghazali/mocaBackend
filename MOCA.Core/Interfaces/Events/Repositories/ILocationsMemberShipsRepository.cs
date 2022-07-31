using MOCA.Core.Entities.LocationManagment;

namespace MOCA.Core.Interfaces.Events.Repositories
{
    public interface ILocationsMemberShipsRepository
    {
        Task<Location> GetLocationByID(long? id);
        Task<List<Location>> GetLocationmoca();
        Task<List<Location>> GetLocationNotmoca();
        Task<Location> GetLocationByIDAndLocType(long id, int LocType);
        Task<int> GetLocationTypeByID(long id);
        Task<bool> LocationTypeExists(int LocType);
    }
}
