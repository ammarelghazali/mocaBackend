using Microsoft.AspNetCore.Http;
using MOCA.Core.Interfaces.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Services.Implementation.Shared
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            var User_Id = httpContextAccessor.HttpContext?.User?.FindFirst("uid").Value.ToString();
            if (!string.IsNullOrEmpty(User_Id)) { this.UserId = User_Id.ToString(); }

           /*var User_Name = httpContextAccessor.HttpContext?.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            if (!string.IsNullOrEmpty(User_Name.ToString())) { this.UserName = User_Name.ToString(); }

            var EMail = httpContextAccessor.HttpContext?.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            if (!string.IsNullOrEmpty(EMail.ToString())) { this.Email = EMail.ToString(); }*/
        }

        public string UserName { get; }
        public string UserId { get; }
        public string Email { get; }
    }
}
