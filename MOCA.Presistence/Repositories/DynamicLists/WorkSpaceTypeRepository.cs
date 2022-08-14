using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Interfaces.DynamicLists.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.DynamicLists
{
    public class WorkSpaceTypeRepository : GenericRepository<WorkSpaceType>, IWorkSpaceTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public WorkSpaceTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteWorkSpaceType(long Id)
        {
            var workSpaceType = _context.WorkSpaceTypes.Where(x => x.Id == Id && x.IsDeleted == false).FirstOrDefault();
            if (workSpaceType == null)
            {
                return false;
            }
            _context.WorkSpaceTypes.Remove(workSpaceType);
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
