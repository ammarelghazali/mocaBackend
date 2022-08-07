using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.DTOs.SSO.User
{
    public class RefreshTokenDto
    {
        [Required]
        public string JwtToken { get; set; }
    }

    
}
