using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.DynamicLists.Repositories
{
    public interface IWorkSpaceTypeRepository : IGenericRepository<WorkSpaceType>
    {
        Task<bool> IsUniqueNameAsync(string workSpaceName);

    }
}
