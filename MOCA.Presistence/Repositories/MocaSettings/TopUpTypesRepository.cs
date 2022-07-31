using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.MocaSettings
{

    public class TopUpTypesRepository : GenericRepository<TopUpType>, ITopUpTypesRepository
    {
        private readonly ApplicationDbContext _context;
        public TopUpTypesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IList<TopUpType>> GetAllBaseAsync()
        {
            var topUpTypes = await _context.TopUpTypes.Where(x => x.IsDeleted == false).ToListAsync();
            return topUpTypes;
        }

        public async Task<TopUpType> GetByName(string name)
        {
            var topUpType = await _context.TopUpTypes.Where(x => x.IsDeleted == false && x.Name == name).FirstOrDefaultAsync();
            return topUpType;
        }

        public async Task<bool> UpdateRelatedTopUps(long oldId, long newId)
        {
            var topUps = await _context.TopUps.Where(x => x.IsDeleted == false && x.TopUpTypeId == oldId).ToListAsync();

            if (topUps.Count > 0)
            {
                foreach (var topUp in topUps)
                {
                    topUp.TopUpTypeId = newId;
                    //topUp.UpdatedAt = DateTime.UtcNow;
                    //topUp.UpdatedBy = user;
                }

                _context.TopUps.UpdateRange(topUps);

                return true;
            }

            return false;
        }


        public async Task<bool> DeleteRelatedTopUps(long TopUpTypeId)
        {
            var topUps = await _context.TopUps.Where(x => x.IsDeleted == false && x.TopUpTypeId == TopUpTypeId).ToListAsync();

            if (topUps.Count > 0)
            {
                //foreach (var topUp in topUps)
                //    topUp.IsDeleted = true;

                _context.TopUps.RemoveRange(topUps);

                return true;
            }

            return false;

        }
    }
}
