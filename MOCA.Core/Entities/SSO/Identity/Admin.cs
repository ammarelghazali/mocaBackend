using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.SSO.Identity
{
    [Table("Admin")]
    public class Admin : IdentityUser
    {
        [Required]
        public string First_Name { get; set; }
        [Required]
        public string Last_Name { get; set; }

        public string RefreshToken { get; set; }
    }
}
