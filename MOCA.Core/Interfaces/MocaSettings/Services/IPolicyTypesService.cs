using MOCA.Core.DTOs.MocaSettings.PolicyTypesDtos.Request;
using MOCA.Core.DTOs.MocaSettings.PolicyTypesDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.MocaSettings.Services
{
    public interface IPolicyTypesService
    {
        Task<Response<PolicyTypeDto>> AddPolicyType(PolicyTypeForCreationDto policyTypeForCreationDto);
        Task<Response<PolicyTypeDto>> UpdatePolicyType(long id, PolicyTypeForCreationDto policyTypeDto);
        Task<Response<bool>> DeletePolicyType(long policyTypeId);
        Task<Response<object>> GetAllPolicyTypes(bool withRelatedDescription, long? LobSpaceTypeId);
        Task<Response<PolicyTypeWithDescriptionDto>> GetSinglePolicyType(long PolicyTypeId);
    }
}
