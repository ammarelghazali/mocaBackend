
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOCA.Core.DTOs.Shared.ThirdParty.Email;
using MOCA.Core.Interfaces.Shared.Services.ThirdParty.Email;
using MOCA.Core.Settings;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MOCA.Services.Implementation.Shared
{
    public class EmailService : IEmailService
    {
        public MailSettings _mailSettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task SendAsync(EmailRequest request, bool IsRelay)
        {

            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();
            if (request.From != null)
            {
                m.From = new MailAddress(request.From, request.UserName);
            }
            else
            {
                m.From = new MailAddress("contact@moca-spaces.com", "moca spaces");
                /*
                 * LIVE
                m.From = new MailAddress("contactus@copolitan.com", "Copolitan");
                */
                //TEST
                //m.From = new MailAddress("contactus@copolitan.com", "Copolitan");
                //m.From = new MailAddress("admin@techno-politan.xyz", "Copolitan");
            }

            if (request.CC != null)
            {
                string[] ccList = request.CC.Split("; ");
                foreach (string cc in ccList)
                {
                    m.CC.Add(cc);
                }
            }

            m.To.Add(request.To);
            m.Subject = request.Subject;
            m.IsBodyHtml = true;
            m.Body = request.Body;
            sc.Host = "relay-hosting.secureserver.net";

            if (request.From != null)
            {
                sc.Credentials = new System.Net.NetworkCredential(request.From, request.Password);
            }
            else
            {
                //sc.Credentials = new System.Net.NetworkCredential("contact@moca-spaces.com", "Conmocas_2022");
                // TEST
                sc.Credentials = new System.Net.NetworkCredential("contactus@copolitan.com", "ContactCopUs_2021");
                //sc.Credentials = new System.Net.NetworkCredential("admin@techno-politan.xyz", "TechXYZ-ADM_2021");
            }

            string userState = "test message1";
            if (request.To.Contains("gmail.com"))
            {
                try
                {
                    sc.Port = 25;
                    
                    sc.Send(m);

                }
                catch (Exception ex)
                {
                    //throw;
                    _logger.LogError(ex.Message, ex);
                    throw new Exception(ex.Message, ex);
                }
            }
            else
            {
                try
                {
                    sc.Port = 25;
                    sc.Send(m);
                }
                catch (Exception ex)
                {
                    //throw;
                    _logger.LogError(ex.Message, ex);
                    throw new Exception(ex.Message, ex);
                }
            }
        }
    }
}
