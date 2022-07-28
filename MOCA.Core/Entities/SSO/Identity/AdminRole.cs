using Microsoft.AspNetCore.Identity;

namespace MOCA.Core.Entities.SSO.Identity
{
    public class AdminRole : IdentityRole
    {
        public AdminRole(string roleName) : base(roleName)
        {
        }
        public AdminRole()
        {

        }
    }
}
