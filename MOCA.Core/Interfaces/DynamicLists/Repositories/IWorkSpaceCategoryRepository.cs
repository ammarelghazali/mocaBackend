using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Interfaces.DynamicLists.Repositories
{
    public interface IWorkSpaceCategoryRepository : IGenericRepository<WorkSpaceCategory>
    {
        Task<bool> DeleteWorkSpaceCategory(long Id);

    }

}


