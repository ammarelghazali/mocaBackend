using MOCA.Core.DTOs.Shared.ThirdParty.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Interfaces.Shared.Services.ThirdParty.Email
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request, bool IsRelay);
    }
}
