using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.MocaSettings
{
    public class PolicyRepository : Repository<Policy>, IPolicyRepository
    {
        private readonly ApplicationDbContext _context;

        public PolicyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<Policy>> GetAllPoliciesAsync(long? lobSpaceTypeId)
        {
            return await _context.Policies.Where(p => p.IsDeleted != true &&
                                                      p.LobSpaceTypeId == lobSpaceTypeId)
                                          .Include(p => p.PolicyType).ToListAsync();
        }

        public async Task<Policy> GetPolicyByIdAndLobId(long id, long? lobSpaceTypeId)
        {
            return await _context.Policies.Where(p => p.IsDeleted != true &&
                                                      p.LobSpaceTypeId == lobSpaceTypeId &&
                                                      p.Id == id)
                                          .FirstOrDefaultAsync();
        }

        public async Task<Policy> GetPolicyByTypeIdAsync(long typeId, long? lobSpaceTypeId)
        {
            return await _context.Policies.Where(p => p.IsDeleted != true &&
                                                      p.PolicyTypeId == typeId &&
                                                      p.LobSpaceTypeId == lobSpaceTypeId)
                                          .FirstOrDefaultAsync();
        }

        public async Task<bool> PolicyExistsAsync(long id)
        {
            return await _context.Policies.AnyAsync(p => p.IsDeleted != true &&
                                                         p.Id == id);
        }

        public async Task<bool> PolicyExistsAsync(long id, long typeId, long? lobSpaceTypeId)
        {
            return await _context.Policies.AnyAsync(p => p.IsDeleted != true &&
                                                         p.Id == id &&
                                                         p.PolicyTypeId == typeId &&
                                                         p.LobSpaceTypeId == lobSpaceTypeId);
        }
    }
}
