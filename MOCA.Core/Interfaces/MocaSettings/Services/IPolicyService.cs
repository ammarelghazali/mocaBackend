using MOCA.Core.DTOs.MocaSettings.LobSpaceTypeDtos;
using MOCA.Core.DTOs.MocaSettings.PoliciesDtos.Requests;
using MOCA.Core.DTOs.MocaSettings.PoliciesDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.MocaSettings.Services
{
    public interface IPolicyService
    {
        Task<Response<PolicyDto>> AddPolicyAsync(PolicyForCreationDto policyForCreationDto,
                                         long policyTypeId);
        Task<Response<PolicyDto>> UpdatePolicyAsync(long policyId,
                                            PolicyForCreationDto policyForCreationDto);
        Task<Response<bool>> DeletePolicyAsync(long policyId, LobSpaceTypeIdDto spaceTypeDto);
        Task<Response<PolicyDto>> GetPolicyByIdAsync(long policyId);
        Task<Response<PolicyDto>> GetPolicyByTypeIdAsync(long policyTypeId,
                                                 LobSpaceTypeIdDto spaceTypeDto);
        Task<Response<IReadOnlyList<PolicyExtendedDto>>> GetAllPoliciesAsync(long? lobSpaceTypeId);
    }
}
