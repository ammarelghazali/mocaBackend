using MOCA.Core.Entities.SSO;
using MOCA.Core.Interfaces.SSO.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Presistence.Repositories.SSO
{
    public class ClientDeviceRepository : GenericRepository<ClientDevice>, IClientDeviceRepository
    {
        private readonly ApplicationDbContext _context;
        public ClientDeviceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
