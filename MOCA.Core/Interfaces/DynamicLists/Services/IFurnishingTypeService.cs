
using MOCA.Core.DTOs.DynamicLists;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;

namespace MOCA.Core.Interfaces.DynamicLists.Services
{
    public interface IFurnishingTypeService
    {
        Task<Response<long>> AddFurnishingType(FurnishingTypeModel request);
        Task<Response<List<FurnishingType>>> AddListOfFurnishingType(List<FurnishingTypeModel> request);
        Task<Response<bool>> UpdateFurnishingType(FurnishingTypeModel request);

        Task<PagedResponse<List<FurnishingTypeModel>>> GetAllFurnishingTypePaginated(RequestParameter filter);

        Task<Response<List<FurnishingTypeModel>>> GetFurnishingTypeWithoutPagination();
        Task<Response<FurnishingTypeModel>> GetFurnishingTypeById(long Id);
        Task<Response<bool>> DeleteFurnishingType(long Id);


    }
}
