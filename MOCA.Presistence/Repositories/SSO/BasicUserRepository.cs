using MOCA.Core.Entities.SSO;
using MOCA.Core.Interfaces.SSO.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Presistence.Repositories.SSO
{
    public class BasicUserRepository : GenericRepository<BasicUser>, IBasicUserRepository
    {
        private readonly ApplicationDbContext _context;
        public BasicUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<long> GetNewUserId()
        {
            long UserId = _context.BasicUsers.OrderByDescending(x => x.Id).Select(C => C.Id).FirstOrDefault();
            return UserId + 1;
        }
        public async Task<BasicUser> GetClientByMobileNoOrEmail(string mobile, string Email)
        {
            return await _context.BasicUsers.Where(a => (a.MobileNumber == mobile || a.Email == Email) && a.IsVerified == true).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<BasicUser> getFirstBasicUserByEmail(string email)
        {
            return await _context.BasicUsers.FirstOrDefaultAsync(m => m.Email == email);
        }
        public async Task<BasicUser> getFirstBasicUserById(long Id)
        {
            return await _context.BasicUsers.FirstOrDefaultAsync(m => m.Id == Id);
        }


    }
}
