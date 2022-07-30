using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.MocaSettings.PoliciesDtos.Response;
using MOCA.Core.DTOs.MocaSettings.PolicyTypesDtos.Request;
using MOCA.Core.DTOs.MocaSettings.PolicyTypesDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MOCA.Services.Implementation.MocaSettings
{
    public class PolicyTypesService : IPolicyTypesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PolicyTypesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Response<PolicyTypeDto>> AddPolicyType(PolicyTypeForCreationDto policyTypeForCreationDto)
        {
            if (await _unitOfWork.PolicyTypes.PolicyTypeExistsAsync(policyTypeForCreationDto.Name))
            {
                return new Response<PolicyTypeDto>
                {
                    Message = "Policy Type with The Same Name Exists"
                };
            }

            var policyType = new PolicyType
            {
                Name = policyTypeForCreationDto.Name,
                URL = policyTypeForCreationDto.URL,
            };

            _unitOfWork.PolicyTypes.Insert(policyType);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<PolicyTypeDto>
                {
                    Message = "Server Cannot Save Resource Right now",
                };
            }

            return new Response<PolicyTypeDto>
            {
                Message = "Policy Type Added Successfully",
                Data = _mapper.Map<PolicyTypeDto>(policyType)
            };
        }

        public async Task<Response<bool>> DeletePolicyType(long policyTypeId)
        {
            var policyType = await _unitOfWork.PolicyTypes.GetByIdAsync(policyTypeId);

            if (policyType == null || policyType.IsDeleted)
            {
                return new Response<bool>
                {
                    Message = "There's no such Policy Type"
                };
            }

            // Delete the policy type
            _unitOfWork.PolicyTypes.Delete(policyType);

            await _unitOfWork.PolicyTypes.DeleteRelatedPolicy(policyTypeId);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>
                {
                    Message = "Server Cannot Save Resource Right now",
                };
            }

            return new Response<bool>
            {
                Data = true,
                Message = "Delete Policy Type Successfully"
            };

        }

        public async Task<Response<object>> GetAllPolicyTypes(bool withRelatedDescription,
                                                         long? lobSpaceTypeId)
        {
            IList<PolicyType> policyTypes;

            if (withRelatedDescription)
            {
                policyTypes = await _unitOfWork.PolicyTypes
                                         .GetAllPolicyTypesWithPolicyDescriptionAsync(lobSpaceTypeId);


                var policiesWithDescription = _mapper
                                              .Map<IReadOnlyList<PolicyTypeWithDescriptionDto>>(policyTypes);

                for (int i = 0; i < policiesWithDescription.Count; i++)
                {
                    policiesWithDescription[i].Policy = _mapper
                                .Map<PolicyDtoMinimized>(policyTypes[i].Policy);
                }

                return new Response<object>
                {
                    Data = policiesWithDescription
                };
            }

            policyTypes = await _unitOfWork.PolicyTypes.GetAllAsync();

            return new Response<object>
            {
                Data = _mapper.Map<IList<PolicyTypeDto>>(policyTypes)
            };
        }

        public async Task<Response<PolicyTypeWithDescriptionDto>> GetSinglePolicyType(long PolicyTypeId)
        {
            var policyType = await _unitOfWork.PolicyTypes
                                              .GetPolicyTypeByIdWithPolicyAsync(PolicyTypeId);

            if (policyType == null || policyType.IsDeleted)
            {
                return new Response<PolicyTypeWithDescriptionDto>
                {
                    Message = "There's no such Policy Type"
                };
            }

            var policyTypeWithRelatedDescription = _mapper.Map<PolicyTypeWithDescriptionDto>(policyType);
            policyTypeWithRelatedDescription.Policy = _mapper.Map<PolicyDtoMinimized>(policyType.Policy);

            return new Response<PolicyTypeWithDescriptionDto>
            {
                Data = policyTypeWithRelatedDescription
            };
        }


        public async Task<Response<PolicyTypeDto>> UpdatePolicyType(long id, PolicyTypeForCreationDto policyTypeDto)
        {
            var oldPolicyType = await _unitOfWork.PolicyTypes.GetByIdAsync(id);

            if (oldPolicyType == null || oldPolicyType.IsDeleted)
            {
                return new Response<PolicyTypeDto>
                {
                    Message = "There's no such Policy Type"
                };
            }

            // Check if there's another policy type with the same name
            if (await _unitOfWork.PolicyTypes.PolicyTypeExistsAsync(policyTypeDto.Name, id))
            {
                return new Response<PolicyTypeDto>
                {
                    Message = "Policy Type with The Same Name Exists"
                };
            }

            // Delete the old policy 
            _unitOfWork.PolicyTypes.Delete(oldPolicyType);

            // Add the updated policy
            var policyType = new PolicyType
            {
                Name = policyTypeDto.Name,
            };

            _unitOfWork.PolicyTypes.Insert(policyType);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<PolicyTypeDto>
                {
                    Message = "Server Cannot Save Resource Right now",
                };
            }

            var isRelatedPolicyUpdated = await _unitOfWork.PolicyTypes
                                        .UpdateRelatedPolicy(id,
                                                             policyType.Id);

            if (isRelatedPolicyUpdated)
            {
                if (await _unitOfWork.SaveAsync() < 1)
                {
                    return new Response<PolicyTypeDto>
                    {
                        Message = "Server Cannot Update Resource Right now",
                    };
                }
            }

            return new Response<PolicyTypeDto>
            {
                Message = "Policy Type Updated Successfully",
                Data = _mapper.Map<PolicyTypeDto>(policyType)
            };
        }
    }
}
