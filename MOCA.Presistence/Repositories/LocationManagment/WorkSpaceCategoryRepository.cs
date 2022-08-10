using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Interfaces.DynamicLists.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class WorkSpaceCategoryRepository : GenericRepository<WorkSpaceCategory>, IWorkSpaceCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public WorkSpaceCategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteWorkSpaceCategory(long Id)
        {
            var workSpaceCategory = _context.WorkSpaceCategories.Where(x => x.Id == Id && x.IsDeleted != true).FirstOrDefault();
            if (workSpaceCategory == null)
            {
                return false;
            }
            _context.WorkSpaceCategories.Remove(workSpaceCategory);
            return true;
        }

        public async Task<bool> IsUniqueNameAsync(string workSpaceName)
        {

            var workSpaceType = _context.WorkSpaceTypes.Where(x => x.Name.Equals(workSpaceName) && x.IsDeleted != true).FirstOrDefault();
            if (workSpaceType == null)
            {
                return true;
            }
            return false;

        }


    }
}
