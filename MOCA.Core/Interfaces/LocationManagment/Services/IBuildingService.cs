using MOCA.Core.DTOs.LocationManagment.Building;
using MOCA.Core.DTOs.LocationManagment.Building.FilterParameter;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface IBuildingService
    {
        Task<Response<long>> AddBuilding(BuildingModel request);
        Task<Response<long>> UpdateBuilding(BuildingModel request);
        Task<Response<bool>> DeleteBuilding(long Id);
        Task<Response<BuildingModel>> GetBuildingByID(long Id);
        Task<Response<List<BuildingModelByLocationId>>> GetBuildingByLocationId(long LocationId);
        Task<PagedResponse<List<GetBuildingModel>>> GetAllBuildingPaginated(RequestParameter filter);
        Task<PagedResponse<List<GetBuildingModel>>> GetAllFilterBuildingPaginated(GetPaginatedBuildingFilterParameter filter);
        Task<Response<List<GetBuildingModel>>> GetAllBuildingWithoutPagination();
        Task<Response<List<GetBuildingModel>>> GetAllFilterBuildingWithoutPagination(GetWithoutPaginatedBuildingFilterParameter filter);
    }
}
