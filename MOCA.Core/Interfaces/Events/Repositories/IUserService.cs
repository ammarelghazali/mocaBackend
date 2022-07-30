using MOCA.Core.DTOs.Events.Account.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.Events.Repositories
{
    public interface IUserService
    {
        Task<Response<UserResponse>> GetUserByID(string uID);
    }
}
