using MOCA.Core.Entities.SSO;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.SSO.Repositories
{
    public interface IBasicUserRepository : IGenericRepository<BasicUser>
    {
        Task<long> GetNewUserId();

        Task<BasicUser> getFirstBasicUserByEmail(string email);

        Task<BasicUser> getFirstBasicUserById(long Id);

        Task<BasicUser> GetClientByMobileNoOrEmail(string mobile, string Email);

    }
}
