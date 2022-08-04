using MOCA.Core.DTOs.LocationManagment.FavouriteLocation;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface IFavouriteLocationService
    {
        Task<Response<long>> AddFavouriteLocation(FavouriteLocationModel request);

        Task<Response<bool>> DeleteFavouriteLocation(long LocationId, long BasicUserID);
    }
}
