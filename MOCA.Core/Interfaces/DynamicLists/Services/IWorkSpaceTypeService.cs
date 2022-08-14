
using MOCA.Core.DTOs.DynamicLists;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.DynamicLists;

namespace MOCA.Core.Interfaces.DynamicLists.Services
{
    public interface IWorkSpaceTypeService
    {
        Task<Response<long>> AddWorkSpaceType(WorkSpaceTypeModel request);
        Task<Response<List<WorkSpaceType>>> AddListOfWorkSpaceTypes(List<WorkSpaceTypeModel> request);
        Task<Response<bool>> UpdateWorkSpaceType(WorkSpaceTypeModel request);

        Task<PagedResponse<List<WorkSpaceTypeModel>>> GetAllWorkSpaceTypePaginated(RequestParameter filter);

        Task<Response<List<WorkSpaceTypeResponseModel>>> GetWorkSpaceTypesWithoutPagination();
        Task<Response<WorkSpaceTypeModel>> GetWorkSpaceTypeById(long Id);
        Task<Response<bool>> DeleteWorkSpaceType(long Id);
    }
}
