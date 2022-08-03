using MOCA.Core.DTOs.Events.EventRequesterDtos.Request;
using MOCA.Core.DTOs.Events.EventRequesterDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.Events.Services
{
    public interface IEventRequesterService
    {
        Task<PagedResponse<IReadOnlyList<GetAllEventRequesterResponseDto>>> GetAllEventRequester(GetAllEventRequesterDto filter);
    }
}
