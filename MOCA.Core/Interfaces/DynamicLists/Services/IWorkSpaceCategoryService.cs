using MOCA.Core.DTOs.DynamicLists;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.DynamicLists;

namespace MOCA.Core.Interfaces.DynamicLists.Services
{
    public interface IWorkSpaceCategoryService
    {
    
         Task<Response<long>> AddWorkSpaceCategory(WorkSpaceCategoryModel request);
        Task<Response<bool>> UpdateWorkSpaceCategory(WorkSpaceCategoryModel request);
        
        Task<Response<bool>> DeleteWorkSpaceCategory(long Id);
        Task<Response<WorkSpaceCategoryModel>> GetWorkSpaceCategoryByID(long Id);
        Task<Response<List<WorkSpaceCategoryModel>>> GetAllWorkSpaceCategoryWithoutPagination();
        Task<Response<List<WorkSpaceCategory>>> AddListOfWorkSpaceCategory(List<WorkSpaceCategoryModel> request);

    }
}
