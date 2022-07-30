using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.MocaSettings
{
    public class CaseTypesRepository : GenericRepository<CaseType>, ICaseTypesReository
    {
        private readonly ApplicationDbContext _context;
        public CaseTypesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CaseTypeExists(long caseTypeId)
        {
            return await _context.CaseTypes.AnyAsync(c => c.Id == caseTypeId && c.IsDeleted != true);
        }

        public async Task<bool> CaseTypeNameExists(string name)
        {
            return await _context.CaseTypes.AnyAsync(c => c.Name == name && c.IsDeleted != true);
        }

        public async Task<IList<CaseType>> GetAllBaseAsync()
        {
            return await _context.CaseTypes.Where(c => c.IsDeleted != true).ToListAsync();
        }
    }
}
