using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Interfaces.DynamicLists.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Presistence.Repositories.DynamicLists
{
    public class WorkSpaceTypeRepository : GenericRepository<WorkSpaceType>, IWorkSpaceTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public WorkSpaceTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsUniqueNameAsync(string workSpaceName)
        {
            var workSpaceCategory = _context.WorkSpaceTypes.Where(x => x.Name.Equals(workSpaceName) && x.IsDeleted != true).FirstOrDefault();
            if (workSpaceCategory == null)
            {
                return false;
            }
            return true;
        }
    }
}
