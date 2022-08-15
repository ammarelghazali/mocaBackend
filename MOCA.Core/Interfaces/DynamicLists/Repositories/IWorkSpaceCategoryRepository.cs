
using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Interfaces.Base;


namespace MOCA.Core.Interfaces.DynamicLists.Repositories
{
    public interface IWorkSpaceCategoryRepository : IGenericRepository<WorkSpaceCategory>
    {
        Task<bool> DeleteWorkSpaceCategory(long Id);
        Task<bool> IsUniqueNameAsync(string workSpaceName);
        Task<bool> DeleteWorkSpaceType(long WorkSpaceCategoryId);
    }

}


