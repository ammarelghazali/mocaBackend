using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.DTOs.SSO.Admin
{
    public class UserClaimModel
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public bool IsSelected { get; set; }
    }
}
