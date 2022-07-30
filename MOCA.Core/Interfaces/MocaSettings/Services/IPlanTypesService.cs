using MOCA.Core.DTOs.MocaSettings.PlanTypesDto.Request;
using MOCA.Core.DTOs.MocaSettings.PlanTypesDto.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.MocaSettings.Services
{
    public interface IPlanTypesService
    {
        Task<Response<IReadOnlyList<PlanTypeDto>>> GetAll();
        Task<Response<PlanTypeManipulationResponse>> Add(PlanTypeForCreationDto planTypeDto);
        Task<Response<bool>> Delete(long id);
        Task<Response<PlanTypeManipulationResponse>> Update(long id, PlanTypeForCreationDto planTypeDto);

    }
}
