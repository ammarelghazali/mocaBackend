using MOCA.Core.DTOs.MocaSettings.StatusDto.Request;
using MOCA.Core.DTOs.MocaSettings.StatusDto.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.MocaSettings.Services
{
    public interface IStatusesService
    {
        Task<Response<IReadOnlyList<StatusDto>>> GetAllStatusesAsync();
        Task<Response<StatusDto>> GetSingleStatusAsync(long statusId);
        Task<Response<StatusDto>> AddStatusyAsync(StatusForCreationDto statusForCreationDto);
        Task<Response<bool>> UpdateStatusAsync(long statusId, StatusForCreationDto statusForCreationDto);
        Task<Response<bool>> DeleteStatusyAsync(long statusId);
    }
}
