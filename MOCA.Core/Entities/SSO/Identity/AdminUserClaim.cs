using Microsoft.AspNetCore.Identity;

namespace MOCA.Core.Entities.SSO.Identity
{
    public class AdminUserClaim : IdentityUserClaim<string>
    {
        public bool? Selected { get; set; }
    }
}
