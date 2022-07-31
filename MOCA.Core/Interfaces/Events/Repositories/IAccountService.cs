using MOCA.Core.DTOs.Events.Account.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.Events.Repositories
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> GetUserById(string userId);
    }
}
