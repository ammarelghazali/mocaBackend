using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.MocaSettings
{
    public class PrioritiesRepository : GenericRepository<Priority>, IPrioritiesRepository
    {
        private readonly ApplicationDbContext _context;
        public PrioritiesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<Priority>> GetAllBaseAsync()
        {
            return await _context.Priorities.Where(p => p.IsDeleted != true).ToListAsync();
        }

        public async Task<bool> PriorityExists(long priorityId)
        {
            return await _context.Priorities.AnyAsync(p => p.Id == priorityId && p.IsDeleted != true);
        }

        public async Task<bool> PriorityNameExists(string name)
        {
            return await _context.Priorities.AnyAsync(p => p.Name == name && p.IsDeleted != true);
        }
    }
}
