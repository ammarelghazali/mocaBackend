using MOCA.Core.DTOs.MocaSettings.SeverityDtos.Request;
using MOCA.Core.DTOs.MocaSettings.SeverityDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.MocaSettings.Services
{
    public interface ISeveritiesService
    {
        Task<Response<IReadOnlyList<SeverityDto>>> GetAllSeverityAsync();
        Task<Response<SeverityDto>> GetSingleSeverityAsync(long statusId);
        Task<Response<SeverityDto>> AddSeverityAsync(SeverityForCreationDto severityForCreationDto);
        Task<Response<bool>> UpdateSeverityAsync(long severityId, SeverityForCreationDto severityForCreationDto);
        Task<Response<bool>> DeleteSeverityAsync(long severityId);
    }
}

