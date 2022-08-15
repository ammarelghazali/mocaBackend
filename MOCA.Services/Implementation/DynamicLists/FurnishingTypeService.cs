

using MOCA.Core.DTOs.DynamicLists;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.DynamicLists.Services;

namespace MOCA.Services.Implementation.DynamicLists
{
    public class FurnishingTypeService : IFurnishingTypeService
    {
        public Task<Response<long>> AddFurnishingType(FurnishingTypeModel request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<FurnishingType>>> AddListOfFurnishingType(List<FurnishingTypeModel> request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> DeleteFurnishingType(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<List<FurnishingTypeModel>>> GetAllFurnishingTypePaginated(RequestParameter filter)
        {
            throw new NotImplementedException();
        }

        public Task<Response<FurnishingTypeModel>> GetFurnishingTypeById(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<FurnishingTypeModel>>> GetFurnishingTypeWithoutPagination()
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> UpdateFurnishingType(FurnishingTypeModel request)
        {
            throw new NotImplementedException();
        }
    }
}
