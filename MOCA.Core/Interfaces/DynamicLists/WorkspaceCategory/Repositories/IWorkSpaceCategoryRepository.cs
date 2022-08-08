using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.DynamicLists.WorkspaceCategory.Repositories
{
    public interface IWorkSpaceCategoryRepository : IGenericRepository<WorkSpaceCategory>
    {
        
        Task<bool> DeleteWorkSpaceCategory(long Id);
    }
}
