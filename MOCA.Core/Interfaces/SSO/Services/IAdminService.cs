using MOCA.Core.DTOs.SSO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Interfaces.SSO.Services
{
    public interface IAdminService
    {
        public Task<string> JwtGeneration(string email);
        public Task<string> RefreshToken(string Token);

        public Task<UserClaimViewModel> GetAllAdminClaims(string Id);

        public Task<int> UpdateAdminClaims(UserClaimViewModel Input);
    }
}
