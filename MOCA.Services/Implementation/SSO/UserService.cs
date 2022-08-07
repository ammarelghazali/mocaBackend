using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MOCA.Core;
using MOCA.Core.DTOs.Shared.Exceptions;
using MOCA.Core.DTOs.SSO.User;
using MOCA.Core.Interfaces.Shared.Services;
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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork,
                            IMapper mapper,
                            IDateTimeService dateTimeService,
                            IConfiguration configuration)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        public async Task<string> JwtGeneration(string email)
        {
            var user = await _unitOfWork.BasicUserRepository.getFirstBasicUserByEmail(email);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            double min = Convert.ToDouble(_configuration["JWTSettings:DurationInMinutes"]);
            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.FirstName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Iss, _configuration["JWTSettings:Issuer"].ToString()),
                    new Claim(JwtRegisteredClaimNames.Aud, _configuration["JWTSettings:Audience"].ToString()),
                    new Claim("uid", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(min),
                SigningCredentials = signingCredentials

            };
            var token = jwtTokenHandler.CreateToken(tokenDes);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }

        public async Task<bool> ChangePassword(long Uid, string newPassword)
        {
            var user = await _unitOfWork.BasicUserRepository.getFirstBasicUserById(Uid);
            var HashPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.Password = HashPassword;
            try
            {
                _unitOfWork.BasicUserRepository.Update(user);
                await _unitOfWork.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<RefreshTokenRespose> RefreshToken(string JwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            JwtToken = JwtToken.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(JwtToken);
            var tokenS = handler.ReadToken(JwtToken) as JwtSecurityToken;
            var clientId = tokenS.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Email).Value;
            var user = await _unitOfWork.BasicUserRepository.getFirstBasicUserByEmail(clientId);
            var device = await _unitOfWork.ClientDeviceRepository.getFirstClientDeviceByUserId(user.Id);
            if (user == null) { throw new ApiException($"Invalid Token"); }

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            double min = Convert.ToDouble(_configuration["JWTSettings:DurationInMinutes"]);
            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.FirstName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Iss, _configuration["JWTSettings:Issuer"].ToString()),
                    new Claim(JwtRegisteredClaimNames.Aud, _configuration["JWTSettings:Audience"].ToString()),
                    new Claim("uid", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(min),
                SigningCredentials = signingCredentials

            };
            var token = jwtTokenHandler.CreateToken(tokenDes);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            var response = new RefreshTokenRespose();
            response.JWToken = jwtToken;
            response.Id = user.Id;
            response.Gender = _unitOfWork.GenderRepository.GetByID(user.Id).Name;
            response.First_Name = user.FirstName;
            response.Last_Name = user.LastName;
            response.Model = device.Model;
            response.OS = device.OS;
            response.Uniquly_Identifier = device.UniqulyIdentifier;
            response.Brand = device.Brand;
            response.DeviceType = device.DeviceType;
            return response;

        }
    }
}
