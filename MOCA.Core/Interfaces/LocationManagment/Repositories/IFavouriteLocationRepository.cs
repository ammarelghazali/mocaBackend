using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface IFavouriteLocationRepository : IGenericRepository<FavouriteLocation>
    {
        Task<bool> IsFavouriteLocation(long LocationID, long BasicUserID);
        Task<bool> DeleteFavouriteLocation(long LocationID, long BasicUserID);
    }
}
