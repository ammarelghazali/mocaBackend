using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.MocaSettings
{
    public class PlansRepository : GenericRepository<Plan>, IPlansRepository
    {
        private readonly ApplicationDbContext _context;
        public PlansRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IList<Plan>> GetAllBaseAsync(long? lobSpaceTypeId)
        {
            var plans = await _context.Plans.Where(p => p.LobSpaceTypeId == lobSpaceTypeId)
                .Where(x => x.IsDeleted == false).Include(x => x.PlanType).ToListAsync();
            return plans;
        }


        public async Task<List<Plan>> GetAllPlansByTypeId(long planTypeId)
        {
            var plans = await _context.Plans.Where(x => x.IsDeleted == false && x.TypeId == planTypeId).ToListAsync();
            return plans;
        }


        public async Task<Plan> GetByType(long lobSpaceTypeId, long typeId)
        {
            var plan = await _context.Plans.Where(p => p.LobSpaceTypeId == lobSpaceTypeId && p.IsDeleted == false
                                            && p.TypeId == typeId)
                                           .Include(p => p.PlanType)
                                           .FirstOrDefaultAsync();
            return plan;
        }

        public async Task<Plan> GetByType(long typeId)
        {
            var plan = await _context.Plans.Where(p => p.LobSpaceTypeId == null && p.IsDeleted == false && p.TypeId == typeId)
                                           .Include(p => p.PlanType)
                                           .FirstOrDefaultAsync();
            return plan;
        }

    }
}