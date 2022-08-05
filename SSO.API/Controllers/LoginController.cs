using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.Shared.Exceptions;
using MOCA.Core.DTOs.SSO.Admin;
using MOCA.Core.Entities.SSO.Identity;
using MOCA.Core.Interfaces.SSO.Services;

namespace SSO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<Admin> _signInManager;
        private readonly UserManager<Admin> _userManager;
        private readonly IAdminService _adminService;

        public LoginController(SignInManager<Admin> signInManager,
                                UserManager<Admin> userManager,
                                IAdminService adminService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _adminService = adminService;
        }

        /// <summary>
        ///     authenticate admin with email and password and returns token + message
        /// </summary>
        /// <param name="tokenRequest">Email and Password for admin</param>
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
        public async Task<IActionResult> Login(TokenRequest tokenRequest)
        {
            var user = await _userManager.FindByEmailAsync(tokenRequest.Email);
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, tokenRequest.Password, false);
                if (result.Succeeded)
                {
                    //string token = await JwtGeneration(user.Email);
                    string token = await _adminService.JwtGeneration(user.Email);


                    return Ok(new TokenResponce()
                    {
                        Token = token,
                        Status = "Authenticated",
                        //RefreshToken = refresh,
                    });
                }
                else
                {
                    return BadRequest(new TokenResponce()
                    {
                        Token = null,
                        Status = "Wrong Email or Password",
                        //RefreshToken = null,
                    });
                }
            }
            else
            {
                return BadRequest(new TokenResponce()
                {
                    Token = null,
                    Status = "Wrong Email or Password",
                    //RefreshToken= null,
                });
            }

        }


        /// <summary>
        /// authenticate user with Jwt Token and returns token + message
        /// </summary>
        /// <param name="tokenRequest"> old JwtToken</param>
        /// <returns>
        /// object {
        ///     Token -> jwt token
        ///     Status -> message for status
        /// }
        /// case 
        ///     code 200 => Authenticated
        ///     code 400 => wrong email or password
        /// </returns>
        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshToken(TokenRefresh tokenRequest)
        {
            string token = await _adminService.RefreshToken(tokenRequest.JwtToken);
            return Ok(new TokenResponce()
            {
                Token = token,
                Status = "Authenticated",
                //RefreshToken = refresh,
            });
        }

        [HttpGet("getAdminClaims")]
        public async Task<IActionResult> GetAdminClaims(GetClaimsRequest Request)
        {
            var admin = await _userManager.FindByIdAsync(Request.Id);
            if (admin == null)
            {
                return BadRequest(new UserClaimViewModel()
                {
                    AdminId = null,
                });
            }

            var returnModel = await _adminService.GetAllAdminClaims(Request.Id);
            return Ok(returnModel);
        }

        [HttpPost("updateAdminClaims")]
        public async Task<IActionResult> UpdateAdminClaims(UserClaimViewModel Request)
        {
            var admin = await _userManager.FindByIdAsync(Request.AdminId);
            if (admin == null)
            {
                return BadRequest(new UserClaimViewModel()
                {
                    AdminId = null,
                });
            }
            try
            {
                var result = await _adminService.UpdateAdminClaims(Request);
                switch (result)
                {
                    case 3:
                        return BadRequest("Error removing old claims");
                    case 2:
                        return BadRequest("Error adding new claims");
                    case 1:
                        return Ok("Done");
                    default:
                        return BadRequest("Error. Please try again");
                }
            }
            catch (Exception ex)
            {

                throw new NotFoundException(ex.Message);
            }
        }

    }
}
