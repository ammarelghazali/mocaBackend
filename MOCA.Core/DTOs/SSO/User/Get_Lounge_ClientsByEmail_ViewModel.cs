using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.DTOs.SSO.User
{
    public class Get_Lounge_ClientsByEmail_ViewModel
    {
        public long Id { get; set; }
        public string JWToken { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Gender { get; set; }
    }
}
