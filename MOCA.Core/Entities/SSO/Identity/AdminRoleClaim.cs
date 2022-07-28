using Microsoft.AspNetCore.Identity;

namespace MOCA.Core.Entities.SSO.Identity
{
    public class AdminRoleClaim : IdentityRoleClaim<string>
    {
        public bool? Selected { get; set; }

    }
}
