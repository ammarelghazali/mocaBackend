using Microsoft.Extensions.Options;
using MOCA.Core.DTOs.Shared.ThirdParty.Winfi;
using MOCA.Core.Interfaces.Shared.Services.ThirdParty.Winfi;
using MOCA.Core.Settings.ThirdParty.Winfi;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Services.ThirdParty.Winfi
{
    public class WinfiService : IWinfiService
    {
        private readonly WinfiSettings _winfiSettings;
        public WinfiService(IOptions<WinfiSettings> winfiSettings)
        {
            _winfiSettings = winfiSettings.Value;
        }

        public async Task<string> RefreshTokenWinfi(string Key)
        {
            
            var clients = new RestClient(_winfiSettings.Url+"auth/"+ _winfiSettings.Key);
            var requests = new RestRequest(Method.GET);
            requests.AddHeader("Content-Type", "application/json");
            IRestResponse<Authentication> restResponse = await clients.ExecuteAsync<Authentication>(requests);
            if (restResponse.IsSuccessful)
            {
                return restResponse.Data.Token;
            }
            return null;
            
        }
        public async Task<string> GetPolicy(string token,string location,string time)
        {
            var clients = new RestClient(_winfiSettings.Url + "company/branch/policy/" +location);
            var requests = new RestRequest(Method.GET);
            requests.AddHeader("Content-Type", "application/json");
            requests.AddHeader("token", token);
            IRestResponse<List<PolicyWinfi>> restResponse = await clients.ExecuteAsync<List<PolicyWinfi>>(requests);
            if (restResponse.IsSuccessful)
            {
                foreach (var policy in restResponse.Data)
                {
                    if (policy.name.Contains(time))
                        return policy.id;
                }               
            }
            return null;
        }
        public async Task<string> CheckIn(string token, string countryCode, string user, string policyId)
        {
            var clients = new RestClient(_winfiSettings.Url + "end-user/check-in");
            var requests = new RestRequest(Method.POST);
            requests.AddHeader("Content-Type", "application/json");
            requests.AddHeader("token", token);
            return null;
        }
    }
}
