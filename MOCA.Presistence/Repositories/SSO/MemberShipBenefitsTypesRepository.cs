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
    public class MemberShipBenefitsTypesRepository : GenericRepository<MemberShipBenefitsTypes>, IMemberShipBenefitsTypesRepository
    {
        private readonly ApplicationDbContext _context;
        public MemberShipBenefitsTypesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
