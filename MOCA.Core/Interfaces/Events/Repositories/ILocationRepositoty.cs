using MOCA.Core.Entities.LocationManagment;

namespace MOCA.Core.Interfaces.Events.Repositories
{
    public interface ILocationRepositoty
    {
        Task<Location> GetLocationByID(long id);
    }
}
