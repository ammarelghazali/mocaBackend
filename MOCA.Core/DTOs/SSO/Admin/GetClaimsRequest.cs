using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.DTOs.SSO.Admin
{
    public class GetClaimsRequest
    {
        [Required]
        public string Id { get; set; }
    }
}
