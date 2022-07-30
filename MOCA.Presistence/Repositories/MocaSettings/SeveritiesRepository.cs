using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.MocaSettings
{
    public class SeveritiesRepository : GenericRepository<Severity>, ISeveritiesRepository
    {
        private readonly ApplicationDbContext _context;
        public SeveritiesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IList<Severity>> GetAllBaseAsync()
        {
            return await _context.Severities.Where(x => x.IsDeleted != true).ToListAsync();
        }

        public async Task<bool> SeverityExists(long SeverityId)
        {
            return await _context.Severities.AnyAsync(s => s.Id == SeverityId && s.IsDeleted != true);
        }

        public async Task<bool> SeverityExists(string SeverityName)
        {
            return await _context.Severities.AnyAsync(s => s.Name == SeverityName && s.IsDeleted != true);
        }
    }
}
