using System;
using System.Collections.Generic;
using System.Text;

namespace MOCA.Core.DTOs.Shared.ThirdParty.Email
{
    public class EmailRequest
    {
        public string To { get; set; }
        public string CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
