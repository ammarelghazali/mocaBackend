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
    public class BasicUserStatusHistoryRepository : GenericRepository<BasicUserStatusHistory> ,IBasicUserStatusHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public BasicUserStatusHistoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
