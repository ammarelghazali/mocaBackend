using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Interfaces.Shared.Services.ThirdParty.Winfi
{
    public interface IWinfiService
    {
        Task<string> RefreshTokenWinfi(string Key);
        Task<string> GetPolicy(string token, string location, string time);
        Task<string> CheckIn(string token, string countryCode, string user, string policyId);
    }
}
