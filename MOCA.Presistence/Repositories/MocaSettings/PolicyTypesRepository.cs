using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.MocaSettings
{
    public class PolicyTypesRepository : GenericRepository<PolicyType>, IPolicyTypesRepository
    {
        private readonly ApplicationDbContext _context;

        public PolicyTypesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<PolicyType>> GetAllAsync()
        {
            return await _context.PolicyTypes.Where(pt => pt.IsDeleted != true).ToListAsync();
        }

        public async Task<IList<PolicyType>> GetAllPolicyTypesWithPolicyDescriptionAsync(
                                                    long? LobSpaceTypeId)
        {
            var policies = await _context.PolicyTypes.Where(pt => pt.IsDeleted != true)
                                                     .ToListAsync();

            foreach (var policy in policies)
            {
                policy.Policy = await _context.Policies.Where(p => p.IsDeleted != true &&
                                                             p.PolicyTypeId == policy.Id &&
                                                             p.LobSpaceTypeId == LobSpaceTypeId)
                                                       .FirstOrDefaultAsync();
            }

            return policies;
        }

        public async Task<PolicyType> GetPolicyTypeByNameWithPolicyAsync(string name)
        {
            return await _context.PolicyTypes.Where(pt => pt.IsDeleted != true &&
                                                          pt.Name == name &&
                                                          pt.Policy.IsDeleted != true)
                                             .FirstOrDefaultAsync();
        }

        public async Task<PolicyType> GetPolicyTypeByIdWithPolicyAsync(long id)
        {
            return await _context.PolicyTypes.Where(pt => pt.IsDeleted != true &&
                                                          pt.Id == id &&
                                                          pt.Policy.IsDeleted != true)
                                             .Include(pt => pt.Policy)
                                             .FirstOrDefaultAsync();
        }

        public async Task<bool> PolicyTypeExistsAsync(long id)
        {
            return await _context.PolicyTypes.AnyAsync(pt => pt.IsDeleted != true &&
                                                       pt.Id == id);
        }

        public async Task<bool> PolicyTypeExistsAsync(string name)
        {
            return await _context.PolicyTypes.AnyAsync(pt => pt.IsDeleted != true &&
                                                       pt.Name == name);
        }

        public async Task<bool> PolicyTypeExistsAsync(string name, long id)
        {
            return await _context.PolicyTypes.AnyAsync(pt => pt.IsDeleted != true &&
                                                       pt.Name == name && pt.Id != id);
        }

        public async Task<bool> UpdateRelatedPolicy(long oldId, long newId)
        {
            var policies = await _context.Policies.Where(p => p.IsDeleted != true &&
                                                              p.PolicyTypeId == oldId)
                                                  .ToListAsync();


            if (policies.Count() != 0)
            {
                foreach (var policy in policies)
                {
                    policy.PolicyTypeId = newId;
                    //policy.UpdatedAt = DateTime.UtcNow;
                    //policy.UpdatedBy = user;
                }

                _context.Policies.UpdateRange(policies);

                return true;
            }

            return false;
        }

        public async Task DeleteRelatedPolicy(long id)
        {
            var policies = await _context.Policies.Where(p => p.IsDeleted != true &&
                                                             p.PolicyTypeId == id)
                                                 .ToListAsync();

            //foreach (var policy in policies)
            //{
            //    policy.IsDeleted = true;
            //    policy.UpdatedAt = DateTime.UtcNow;
            //    policy.UpdatedBy = user;
            //}

            _context.Policies.RemoveRange(policies);
        }
    }
}
