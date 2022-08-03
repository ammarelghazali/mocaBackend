using MOCA.Core.DTOs.Events.EventCategoryDtos.Request;
using MOCA.Core.DTOs.Events.EventCategoryDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.Events.Services
{
    public interface IEventCategoryService
    {
        Task<PagedResponse<IReadOnlyList<GetAllEventCategoryViewModel>>> GetAll(GetAllEventCategoryQuery request);
    }
}
