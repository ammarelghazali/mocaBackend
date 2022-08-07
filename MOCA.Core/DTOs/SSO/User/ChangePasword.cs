using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.DTOs.SSO.User
{
    public class ChangePasword
    {
        [Required]
        [MinLength(6)]
        public string Current_Password { get; set; }
        public string New_Password { get; set; }
        [Required]
        [Compare("New_Password")]
        public string ConfirmPassword { get; set; }
        public long Client_Id { get; set; } //Client id 
    }

    public class changePasswordResponse
    {
        public string message { get; set; }
    }
}
