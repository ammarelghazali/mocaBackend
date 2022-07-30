using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.MocaSettings
{
    public class StatusesRepository : GenericRepository<Status>, IStatusesRepository
    {
        private readonly ApplicationDbContext _context;
        public StatusesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<Status>> GetAllBaseAsync()
        {
            return await _context.Statuses.Where(x => x.IsDeleted != true).ToListAsync();
        }

        public async Task<bool> StatusExists(long statusId)
        {
            return await _context.Statuses.AnyAsync(s => s.Id == statusId && s.IsDeleted != true);
        }

        public async Task<bool> StatusExists(string statusName)
        {
            return await _context.Statuses.AnyAsync(s => s.Name == statusName && s.IsDeleted != true);
        }
    }
}
