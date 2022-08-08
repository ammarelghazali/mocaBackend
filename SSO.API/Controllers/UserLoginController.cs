using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOCA.Core;
using MOCA.Core.DTOs.Shared.Exceptions;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.SSO.User;
using MOCA.Core.Interfaces.SSO.Services;

namespace SSO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public UserLoginController(IUserService userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;

        }

        /// <summary>
        ///     authenticate user with email and password and returns token + message
        /// </summary>
        /// <param name="tokenRequest">Email and Password for user</param>
        /// <returns>
        /// object {
        ///     Token -> jwt token
        ///     Status -> message for status
        /// }
        /// case 
        ///     code 200 => Authenticated
        ///     code 400 => wrong email or password
        /// </returns>
        [HttpPost("Login")]
        public async Task<Response<Get_Lounge_ClientsByEmail_ViewModel>> Login(LoginDto tokenRequest)
        {
            var user = await _unitOfWork.BasicUserRepository.getFirstBasicUserByEmail(tokenRequest.Email);
            bool verified = BCrypt.Net.BCrypt.Verify(tokenRequest.Password, user.Password);
            if (user == null || verified == false)
            {
                throw new ApiException("Wrong email or password");
            }
            if (user.Status == "Deleted")
            {
                return new Response<Get_Lounge_ClientsByEmail_ViewModel>("Account Deleted.");
            }
            if (user.Status == "Deactivated")
            {
                return new Response<Get_Lounge_ClientsByEmail_ViewModel>($"Account Deactivated.");
            }
            if (user.IsVerified == false)
            {
                return new Response<Get_Lounge_ClientsByEmail_ViewModel>($"Account not verified.");
            }
            if (user.StatusUser == true)
            {
                return new Response<Get_Lounge_ClientsByEmail_ViewModel>($"Account Disabled.");
            }

            string token = await _userService.JwtGeneration(user.Email);

            var data = new Get_Lounge_ClientsByEmail_ViewModel();
            data.Id = user.Id;
            data.JWToken = token;
            data.First_Name = user.FirstName;
            data.Last_Name = user.LastName;
            data.Gender = _unitOfWork.GenderRepository.GetByID(user.Gender).Name;
            return new Response<Get_Lounge_ClientsByEmail_ViewModel>(data);
        }

        /// <summary>
        /// change password for a user after checking old password
        /// </summary>
        /// <param name="request">
        /// Current_Password -> old password
        /// New_Password -> new
        /// ConfirmPassword -> confirm
        /// Client_Id -> user id 
        /// </param>
        /// <returns></returns>
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasword request)
        {
            var user = await _unitOfWork.BasicUserRepository.getFirstBasicUserById(request.Client_Id);
            bool verified = BCrypt.Net.BCrypt.Verify(request.Current_Password, user.Password);
            if (user == null || verified == false)
            {
                throw new ApiException("Wrong id or password");
            }
            bool check = await _userService.ChangePassword(request.Client_Id, request.New_Password);
            if (check)
            {
                return Ok(new changePasswordResponse()
                {
                    message = "Changed"
                });
            }
            else
            {
                return BadRequest(new changePasswordResponse()
                {
                    message = "please try again"
                });
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<Response<RefreshTokenRespose>> RefreshToken(RefreshTokenDto request)
        {
            var response = await _userService.RefreshToken(request.JwtToken);
            return new Response<RefreshTokenRespose>(response, "Refresh Token Generated");
        }
    }
}
