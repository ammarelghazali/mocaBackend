using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.MocaSettings.LobSpaceTypeDtos;
using MOCA.Core.DTOs.MocaSettings.PoliciesDtos.Requests;
using MOCA.Core.DTOs.MocaSettings.PoliciesDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MOCA.Services.Implementation.MocaSettings
{
    public class PolicyService : IPolicyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PolicyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Response<PolicyDto>> AddPolicyAsync(PolicyForCreationDto policyForCreationDto,
                                         long policyTypeId)
        {
            if (!await _unitOfWork.PolicyTypes.PolicyTypeExistsAsync(policyTypeId))
            {
                return new Response<PolicyDto>
                {
                    Message = "There's no such Policy Type"
                };
            }

            //if (policyForCreationDto.LobSpaceTypeId is not null)
            //{
            //    if (!await _unitOfWork.LobSpaceTypes.LobSpaceTypeExists(
            //                                               (long)policyForCreationDto.LobSpaceTypeId))
            //    {
            //        return new ResponseDto
            //        {
            //            StatusCode = 400,
            //            Message = "There's no such Policy Type"
            //        };
            //    }
            //}

            var policy = await _unitOfWork.Policies.GetPolicyByTypeIdAsync(policyTypeId,
                                                                  policyForCreationDto.LobSpaceTypeId);

            var newPolicy = new Policy();

            if (policy != null)
            {
                newPolicy = _mapper.Map<Policy>(policy);

                // Delete old policy
                _unitOfWork.Policies.Delete(policy);
            }
            else
            {
                newPolicy.PolicyTypeId = policyTypeId;
                newPolicy.LobSpaceTypeId = policyForCreationDto.LobSpaceTypeId;

            }

            newPolicy.Description = policyForCreationDto.Description;

            _unitOfWork.Policies.Insert(newPolicy);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<PolicyDto>
                {
                    Message = "Server Cannot Save Resource Right now",
                };
            }

            return new Response<PolicyDto>
            {
                Message = "Policy Added Successfully",
                Data = _mapper.Map<PolicyDto>(newPolicy)
            };
        }

        public async Task<Response<bool>> DeletePolicyAsync(long policyId, LobSpaceTypeIdDto spaceTypeDto)
        {
            var policy = await _unitOfWork.Policies.GetPolicyByIdAndLobId(policyId,
                                                                spaceTypeDto.LobSpaceTypeId);

            if (policy == null || policy.IsDeleted)
            {
                return new Response<bool>
                {
                    Message = "There's no such Policy"
                };
            }


            _unitOfWork.Policies.Delete(policy);


            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>
                {
                    Message = "Server Cannot Delete Resource Right now",
                };
            }

            return new Response<bool>
            {
                Data = true,
                Message = "Policy Deleted Successfully"
            };
        }

        public async Task<Response<IReadOnlyList<PolicyExtendedDto>>> GetAllPoliciesAsync(long? LobSpaceTypeId)
        {
            var policies = await _unitOfWork.Policies.GetAllPoliciesAsync(LobSpaceTypeId);

            return new Response<IReadOnlyList<PolicyExtendedDto>>
            {
                Data = _mapper.Map<IReadOnlyList<PolicyExtendedDto>>(policies)
            };
        }

        public async Task<Response<PolicyDto>> GetPolicyByIdAsync(long policyId)
        {
            var policy = await _unitOfWork.Policies.GetByIdAsync(policyId);

            if (policy == null || policy.IsDeleted)
            {
                return new Response<PolicyDto>
                {
                    Message = "There's no such Policy"
                };
            }

            return new Response<PolicyDto>
            {
                Data = _mapper.Map<PolicyDto>(policy)
            };
        }

        public async Task<Response<PolicyDto>> GetPolicyByTypeIdAsync(long policyTypeId,
                                                              LobSpaceTypeIdDto spaceTypeDto)
        {
            if (!await _unitOfWork.PolicyTypes.PolicyTypeExistsAsync(policyTypeId))
            {
                return new Response<PolicyDto>
                {
                    Message = "There's no such Policy Type"
                };
            }

            var policy = await _unitOfWork.Policies.GetPolicyByTypeIdAsync(policyTypeId,
                                                                          spaceTypeDto.LobSpaceTypeId);

            if (policy == null)
            {
                return new Response<PolicyDto>
                {
                    Message = "There's no Policy Description Yet"
                };
            }

            return new Response<PolicyDto>
            {
                Data = _mapper.Map<PolicyDto>(policy)
            };
        }

        public async Task<Response<PolicyDto>> UpdatePolicyAsync(long policyId,
                                            PolicyForCreationDto policyForCreationDto)
        {
            var oldPolicy = await _unitOfWork.Policies.GetPolicyByIdAndLobId(policyId,
                                                            policyForCreationDto.LobSpaceTypeId);

            if (oldPolicy == null || oldPolicy.IsDeleted)
            {
                return new Response<PolicyDto>
                {
                    Message = "There's no such Policy"
                };
            }

            _unitOfWork.Policies.Delete(oldPolicy);

            var newPolicy = _mapper.Map<Policy>(oldPolicy);

            newPolicy.Description = policyForCreationDto.Description;

            _unitOfWork.Policies.Insert(newPolicy);


            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<PolicyDto>
                {
                    Message = "Server Cannot Save Resource Right now",
                };
            }

            return new Response<PolicyDto>
            {
                Message = "Policy Updated Successfully",
                Data = _mapper.Map<PolicyDto>(newPolicy)
            };
        }
    }
}
