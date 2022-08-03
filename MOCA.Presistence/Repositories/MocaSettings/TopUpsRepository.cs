using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.MocaSettings
{
    public class TopUpsRepository : GenericRepository<TopUp>, ITopUpsRespository
    {
        private readonly ApplicationDbContext _context;
        public TopUpsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<TopUp>> GetAllNotDeletedAsync()
        {
            var topUps = await _context.TopUps.Where(x => x.IsDeleted == false)
                                              .Include(x => x.TopUpType)
                                              .ToListAsync();
            return topUps;
        }

        public async Task<List<TopUp>> GetAllByType(long topUpTypeId)
        {
            var topUps = await _context.TopUps.Where(x => x.IsDeleted == false && x.TopUpTypeId == topUpTypeId)
                                             .ToListAsync();
            return topUps;
        }

        public async Task<TopUp> GetByTopUpTypeId(long topUpTypeId)
        {
            var topUp = await _context.TopUps.Where(x => x.IsDeleted == false
                                                    && x.TopUpTypeId == topUpTypeId
                                                    && x.LobSpaceTypeId == null
                                                 )
                                             .FirstOrDefaultAsync();
            return topUp;
        }

        public async Task<TopUp> GetByTopUpTypeId(long topUpTypeId, long lobSpaceTypeId)
        {
            var topUp = await _context.TopUps.Where(x => x.IsDeleted == false
                                                     && x.TopUpTypeId == topUpTypeId
                                                    && x.LobSpaceTypeId == lobSpaceTypeId
                                                    )
                                             .FirstOrDefaultAsync();
            return topUp;
        }

    }
}
