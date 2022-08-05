using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MOCA.Core.DTOs.Shared.Exceptions;
using MOCA.Core.DTOs.SSO.Admin;
using MOCA.Core.Entities.SSO.Identity;
using MOCA.Core.Interfaces.SSO.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Services.Implementation.SSO
{
    public class AdminService : IAdminService
    {
        private readonly SignInManager<Admin> _signInManager;
        private readonly UserManager<Admin> _userManager;
        private readonly IConfiguration _configuration;
        public AdminService(SignInManager<Admin> signInManager, UserManager<Admin> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> JwtGeneration(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = await _userManager.GetClaimsAsync(user);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["JWTSettings:Issuer"].ToString()),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["JWTSettings:Audience"].ToString()),
                new Claim("uid", user.Id),
            }
            .Union(userClaims);

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims
                ),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = signingCredentials

            };
            var token = jwtTokenHandler.CreateToken(tokenDes);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }

        public async Task<string> RefreshToken(string Token)
        {
            var handler = new JwtSecurityTokenHandler();
            Token = Token.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(Token);
            var tokenS = handler.ReadToken(Token) as JwtSecurityToken;
            var clientId = tokenS.Claims.First(claim => claim.Type == "uid").Value;
            //var client = _context.Lounge_Clients.Where(a => a.Id == clientId).FirstOrDefault();
            var client = await _userManager.FindByIdAsync(clientId);
            if (client == null || client.Id != clientId) { throw new ApiException($"Invalid Token"); }
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = await _userManager.GetClaimsAsync(client);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, client.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, client.Email),
                    new Claim(JwtRegisteredClaimNames.Iss, _configuration["JWTSettings:Issuer"].ToString()),
                    new Claim(JwtRegisteredClaimNames.Aud, _configuration["JWTSettings:Audience"].ToString()),
                    new Claim("uid", client.Id),
            }
            .Union(userClaims);

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims
                ),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = signingCredentials

            };
            var token = jwtTokenHandler.CreateToken(tokenDes);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;

        }

        /// <summary>
        /// return userClaimViewModel containing all admin claims
        /// </summary>
        /// <param name="Id">
        ///     admin Id
        /// </param>
        /// <returns>
        ///     UserClaimViewModel 
        ///     {
        ///         adminId
        ///         list of claims with bool if admin have this claim
        ///     }
        /// </returns>
        public async Task<UserClaimViewModel> GetAllAdminClaims(string Id)
        {
            var admin = await _userManager.FindByIdAsync(Id);
            var existingClaims = await _userManager.GetClaimsAsync(admin);
            var returnModel = new UserClaimViewModel
            {
                AdminId = admin.Id
            };
            foreach (Claim claim in ClaimStore.AllClaims)
            {
                UserClaimModel obj = new UserClaimModel
                {
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                };
                if (existingClaims.Any(ec => ec.Type == claim.Type))
                {
                    obj.IsSelected = true;
                }
                returnModel.Claims.Add(obj);
            }
            return returnModel;
        }

        /// <summary>
        ///     takes userClaimsViewModel, removes old list of claims and add updated list
        /// </summary>
        /// <param name="Input">
        ///     admin id
        ///     list of claims model
        /// </param>
        /// <returns>
        ///    int based on status
        ///        1 -> success
        ///        2 -> error in adding new list
        ///        3 -> error in removing old list
        /// </returns>
        public async Task<int> UpdateAdminClaims(UserClaimViewModel Input)
        {
            var admin = await _userManager.FindByIdAsync(Input.AdminId);
            var claims = await _userManager.GetClaimsAsync(admin);
            var result = await _userManager.RemoveClaimsAsync(admin, claims);
            if (!result.Succeeded)
            {
                return 3;
            }
            result = await _userManager.AddClaimsAsync(admin, Input.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimValue)));
            if (!result.Succeeded)
            {
                return 2;
            }
            return 1;
        }

    }
}
