using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface ILocationService
    {
        Task<Response<long>> AddLocation(LocationModel request);

        Task<Response<long>> UpdateLocation(LocationModel request);

        Task<Response<bool>> DeleteLocation(long LocationId);

        Task<Response<LocationRegion>> GetRegionsDropDown();
    }
}
