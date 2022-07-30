using MOCA.Core.DTOs.LocationManagment.Feature;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface IFeatureService
    {
        Task<Response<long>> AddFeature(FeatureModel request);
        Task<Response<bool>> UpdateFeature(FeatureModel request);
        Task<Response<FeatureModel>> GetFeatureByID(long Id);
        Task<PagedResponse<List<FeatureModel>>> GetAllFeaturesWithPagination(RequestParameter filter);
        Task<Response<List<FeatureModel>>> GetAllFeaturesWithoutPagination();
        Task<Response<bool>> DeleteFeature(long Id);
    }
}
