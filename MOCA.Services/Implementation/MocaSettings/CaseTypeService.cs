using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.MocaSettings.CaseTypesDtos.Request;
using MOCA.Core.DTOs.MocaSettings.CaseTypesDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MOCA.Services.Implementation.MocaSettings
{
    public class CaseTypeService : ICaseTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CaseTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<CaseTypeDto>> AddCaseTypeAsync(CaseTypeForCreationDto caseTypeForCreation)
        {
            if (await _unitOfWork.CaseTypes.CaseTypeNameExists(caseTypeForCreation.Name))
            {
                return new Response<CaseTypeDto>
                {
                    Message = "There's a case type with the same name"
                };
            }

            var caseType = new CaseType
            {
                Name = caseTypeForCreation.Name,
            };

            _unitOfWork.CaseTypes.Insert(caseType);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<CaseTypeDto>
                {
                    Message = "Server Failed to Add the Case Type"
                };
            }

            return new Response<CaseTypeDto>
            {
                Data = _mapper.Map<CaseTypeDto>(caseType)
            };
        }

        public async Task<Response<bool>> DeleteCaseTypeAsync(long caseTypeId)
        {
            var caseType = await _unitOfWork.CaseTypes.GetByIdAsync(caseTypeId);

            if (caseType is null || caseType.IsDeleted)
            {
                return new Response<bool>
                {
                    Message = "There's no such case type"
                };
            }

            _unitOfWork.CaseTypes.Delete(caseType);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>
                {
                    Message = "Server Failed to Delete the Case Type"
                };
            }

            return new Response<bool>
            {
                Data = true,
                Message = "Resource Deleted Successfully",
                Succeeded = true
            };
        }

        public async Task<Response<IReadOnlyList<CaseTypeDto>>> GetAllCaseTypesAsync()
        {
            var caseTypes = await _unitOfWork.CaseTypes.GetAllBaseAsync();

            return new Response<IReadOnlyList<CaseTypeDto>>
            {
                Data = _mapper.Map<IReadOnlyList<CaseTypeDto>>(caseTypes)
            };
        }

        public async Task<Response<CaseTypeDto>> GetSingleCaseTypeAsync(long caseTypeId)
        {
            var caseType = await _unitOfWork.CaseTypes.GetByIdAsync(caseTypeId);

            if (caseType is null || caseType.IsDeleted)
            {
                return new Response<CaseTypeDto>
                {
                    Message = "There's no such case type"
                };
            }

            return new Response<CaseTypeDto>
            {
                Data = _mapper.Map<CaseTypeDto>(caseType)
            };
        }

        public async Task<Response<CaseTypeDto>> UpdateCaseTypeAsync(long caseTypeId, CaseTypeForCreationDto caseTypeForCreation)
        {
            var oldCaseType = await _unitOfWork.CaseTypes.GetByIdAsync(caseTypeId);

            if (oldCaseType is null || oldCaseType.IsDeleted)
            {
                return new Response<CaseTypeDto>
                {
                    Message = "There's no such case type"
                };
            }

            if (await _unitOfWork.CaseTypes.CaseTypeNameExists(caseTypeForCreation.Name))
            {
                return new Response<CaseTypeDto>
                {
                    Message = "There's a case type with the same name"
                };
            }

            _unitOfWork.CaseTypes.Delete(oldCaseType);

            var newCaseType = new CaseType
            {
                Name = caseTypeForCreation.Name,
            };

            _unitOfWork.CaseTypes.Insert(newCaseType);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<CaseTypeDto>
                {
                    Message = "Server Failed to Update the Case Type"
                };
            }

            return new Response<CaseTypeDto>
            {
                Data = _mapper.Map<CaseTypeDto>(newCaseType)
            };
        }
    }
}
