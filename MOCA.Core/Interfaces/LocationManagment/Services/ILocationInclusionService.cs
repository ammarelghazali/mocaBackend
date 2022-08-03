using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface ILocationInclusionService
    {
        Task<Response<bool>> AddLocationInclusions(List<LocationInclusionModel> request);
        Task<Response<bool>> DeleteLocationInclusions(long LocationID);
        Task<Response<List<LocationInclusionModel>>> GetLocationInclusionsByLocationID(long LocationID);
    }
}
