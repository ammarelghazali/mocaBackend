using MOCA.Core.DTOs.MocaSettings.CaseTypesDtos.Request;
using MOCA.Core.DTOs.MocaSettings.CaseTypesDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.MocaSettings.Services
{
    public interface ICaseTypeService
    {
        Task<Response<IReadOnlyList<CaseTypeDto>>> GetAllCaseTypesAsync();
        Task<Response<CaseTypeDto>> GetSingleCaseTypeAsync(long caseTypeId);
        Task<Response<CaseTypeDto>> AddCaseTypeAsync(CaseTypeForCreationDto caseTypeForCreation);
        Task<Response<CaseTypeDto>> UpdateCaseTypeAsync(long caseTypeId, CaseTypeForCreationDto caseTypeForCreation);
        Task<Response<bool>> DeleteCaseTypeAsync(long caseTypeId);
    }
}
