using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.MocaSettings
{
    public class PlanTypesRepository : Repository<PlanType>, IPlanTypesRepository
    {
        private readonly ApplicationDbContext _context;
        public PlanTypesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<PlanType>> GetAllTypes()
        {
            var planTypes = await _context.PlanTypes.Where(x => x.IsDeleted == false).ToListAsync();
            return planTypes;
        }

        public async Task<PlanType> GetByName(string name)
        {
            var planType = await _context.PlanTypes.Where(x => x.IsDeleted == false && x.Name == name).FirstOrDefaultAsync();
            return planType;
        }

    }
}
