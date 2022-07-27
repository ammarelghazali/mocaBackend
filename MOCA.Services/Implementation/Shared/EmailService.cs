
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


            //await Task.Factory.StartNew(() =>
            // {

            // create message
            /*var email = new MimeMessage();
            var from = new MailboxAddress(_mailSettings.DisplayName,_mailSettings.EmailFrom);
            email.From.Add(from);

            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text =  request.Body };

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = request.Body;
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, false);
            smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
            smtp.Send(email);
            smtp.Disconnect(true); */
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
            //m.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.OnFailure;
            sc.Host = "relay-hosting.secureserver.net";
            /*if (!IsRelay)
            {
                sc.Host = "smtpout.secureserver.net";
            }
            else if (IsRelay)
            {
                sc.Host = "relay-hosting.secureserver.net";
            }*/
            //sc.Host = "smtpout.secureserver.net";
            //sc.Host = "relay-hosting.secureserver.net";

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
            //sc.UseDefaultCredentials = true;
            if (request.To.Contains("gmail.com"))
            {//System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                try
                {
                    sc.Port = 25;
                    //sc.EnableSsl = true;
                    //sc.UseDefaultCredentials = false;
                    sc.Send(m);
                    // create message
                    //var email = new MimeMessage();
                    //email.Sender = MailboxAddress.Parse(request.From ?? _mailSettings.EmailFrom);
                    //email.To.Add(MailboxAddress.Parse(request.To));
                    //email.Subject = request.Subject;
                    //var builder = new BodyBuilder();
                    //builder.HtmlBody = request.Body;
                    //email.Body = builder.ToMessageBody();
                    //using (SmtpClient smtp = new SmtpClient())
                    //{
                    //    smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                    //    smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                    //    await smtp.SendAsync(email);
                    //    smtp.Disconnect(true);
                    //}

                    // });

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
                    //sc.EnableSsl = false;
                    sc.Send(m);
                    // create message
                    //var email = new MimeMessage();
                    //email.Sender = MailboxAddress.Parse(request.From ?? _mailSettings.EmailFrom);
                    //email.To.Add(MailboxAddress.Parse(request.To));
                    //email.Subject = request.Subject;
                    //var builder = new BodyBuilder();
                    //builder.HtmlBody = request.Body;
                    //email.Body = builder.ToMessageBody();
                    //using (SmtpClient smtp = new SmtpClient())
                    //{
                    //    smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                    //    smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                    //    await smtp.SendAsync(email);
                    //    smtp.Disconnect(true);
                    //}

                    // });

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
