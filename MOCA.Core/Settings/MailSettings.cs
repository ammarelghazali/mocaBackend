using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Settings
{
    public class MailSettings
    {
        public int SmtpPort { get; set; }
        public string SmtpHost { get; set; }
        public string SmtpHostRelay { get; set; }
        public string ContactUsCopolitan { get; set; }
        public string ContactUsTechno { get; set; }
        public string SmtpCopolitanPass { get; set; }
        public string ContactUsMoca { get; set; }
        public string SmtpMocaPass { get; set; }
        public string ContactUsTechnopolitan { get; set; }
        public string SmtpTechnopolitanPass { get; set; }
        public string DisplayNameCopolitan { get; set; }
        public string DisplayNameTechnopolitan { get; set; }
        public string DisplayNameMoca { get; set; }
        public string InfoCopolitanEmail { get; set; }
    }
}
