using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.DTOs.SSO.Admin
{
    public class UserClaimViewModel
    {
        public string AdminId { get; set; }
        public List<UserClaimModel> Claims { get; set; }

        public UserClaimViewModel()
        {
            Claims = new List<UserClaimModel>();
        }
    }
}
