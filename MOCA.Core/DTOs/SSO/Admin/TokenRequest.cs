using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.DTOs.SSO.Admin
{
    public class TokenRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class TokenResponce
    {
        public string Token { get; set; }
        public string Status { get; set; }
    }

    public class TokenRefresh
    {

        [Required]
        public string JwtToken { get; set; }

    }
}
