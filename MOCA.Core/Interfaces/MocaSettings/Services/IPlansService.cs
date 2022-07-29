using MOCA.Core.DTOs.MocaSettings.PlanDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.MocaSettings.Services
{
    public interface IPlansService
    {
        Task<Response<PlanDto>> GetByType(long? LobSpaceTypeId, long planTypeId);
        Task<Response<PlanDtoBase>> Add(long? LobSpaceTypeId, long planTypeId, PlanDtoBase planDto);
        Task<Response<PlanDtoBase>> Update(long? lobSpaceTypeId, long planTypeId, PlanDtoBase planDto);
        Task<Response<bool>> Delete(long? LobSpaceTypeId, long planTypeId);
    }
}
