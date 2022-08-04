using MOCA.Core.DTOs.SSO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Interfaces.SSO.Services
{
    public interface IUserService
    {
        public Task<string> JwtGeneration(string email);
        public Task<bool> ChangePassword(long id, string password);
        public Task<RefreshTokenRespose> RefreshToken(string token);
    }
}
