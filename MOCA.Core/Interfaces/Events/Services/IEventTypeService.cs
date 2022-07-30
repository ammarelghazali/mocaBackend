using MOCA.Core.DTOs.Events.EventTypeDtos.Requset;
using MOCA.Core.DTOs.Events.EventTypeDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.Events.Services
{
    public interface IEventTypeService
    {
        Task<PagedResponse<IReadOnlyList<AllEventTypesDto>>> GetAllEventTypes(GetAllEventTypeDto filter);
    }
}


