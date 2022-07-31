using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.MocaSettings
{
    public class WifisRepository : GenericRepository<Wifi>, IWifisRepository
    {
        private readonly ApplicationDbContext _context;

        public WifisRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Wifi> GetWifiByLobSpaceTypeId(long? lobSpaceTypeId)
        {
            return await _context.Wifis.Where(w => w.IsDeleted != true &&
                                                   w.LobSpaceTypeId == lobSpaceTypeId)
                                       .FirstOrDefaultAsync();
        }
    }
}
