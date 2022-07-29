using MOCA.Core.DTOs.MocaSettings.PriorityDtos.Request;
using MOCA.Core.DTOs.MocaSettings.PriorityDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.MocaSettings.Services
{
    public interface IPriorityService
    {
        Task<Response<IReadOnlyList<PriorityDto>>> GetAllPrioritiesAsync();
        Task<Response<PriorityDto>> GetSinglePriorityAsync(long priorityId);
        Task<Response<PriorityDto>> AddPriorityAsync(PriorityForCreationDto priorityForCreation);
        Task<Response<PriorityDto>> UpdatePriorityAsync(long priorityId, PriorityForCreationDto priorityForCreation);
        Task<Response<bool>> DeletePriorityAsync(long priorityId);
    }
}
