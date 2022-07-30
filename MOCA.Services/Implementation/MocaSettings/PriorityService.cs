using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.MocaSettings.PriorityDtos.Request;
using MOCA.Core.DTOs.MocaSettings.PriorityDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MOCA.Services.Implementation.MocaSettings
{
    public class PriorityService : IPriorityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PriorityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<PriorityDto>> AddPriorityAsync(PriorityForCreationDto priorityForCreation)
        {
            if (await _unitOfWork.Priorities.PriorityNameExists(priorityForCreation.Name))
            {
                return new Response<PriorityDto>
                {
                    Message = "There's a priority with the same name"
                };
            }

            var priority = new Priority
            {
                Name = priorityForCreation.Name,
            };

            _unitOfWork.Priorities.Insert(priority);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<PriorityDto>
                {
                    Message = "Server Failed to Add the priority"
                };
            }

            return new Response<PriorityDto>
            {
                Data = _mapper.Map<PriorityDto>(priority)
            };
        }

        public async Task<Response<bool>> DeletePriorityAsync(long priorityId)
        {
            var priority = await _unitOfWork.Priorities.GetByIdAsync(priorityId);

            if (priority is null || priority.IsDeleted)
            {
                return new Response<bool>
                {
                    Message = "There's no such priority"
                };
            }

            _unitOfWork.Priorities.Delete(priority);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>
                {
                    Message = "Server Failed to Delete the Priority"
                };
            }

            return new Response<bool>
            {
                Data = true,
                Message = "Priority Deleted Successfully"
            };
        }

        public async Task<Response<IReadOnlyList<PriorityDto>>> GetAllPrioritiesAsync()
        {
            var priority = await _unitOfWork.Priorities.GetAllAsync();

            return new Response<IReadOnlyList<PriorityDto>>
            {
                Data = _mapper.Map<IReadOnlyList<PriorityDto>>(priority)
            };
        }

        public async Task<Response<PriorityDto>> GetSinglePriorityAsync(long priorityId)
        {
            var priority = await _unitOfWork.Priorities.GetByIdAsync(priorityId);

            if (priority is null || priority.IsDeleted)
            {
                return new Response<PriorityDto>
                {
                    Message = "There's no such priority"
                };
            }

            return new Response<PriorityDto>
            {
                Data = _mapper.Map<PriorityDto>(priority)
            };
        }

        public async Task<Response<PriorityDto>> UpdatePriorityAsync(long priorityId, PriorityForCreationDto priorityForCreation)
        {
            var oldPriority = await _unitOfWork.Priorities.GetByIdAsync(priorityId);

            if (oldPriority is null || oldPriority.IsDeleted)
            {
                return new Response<PriorityDto>
                {
                    Message = "There's no such priority"
                };
            }

            if (await _unitOfWork.Priorities.PriorityNameExists(priorityForCreation.Name))
            {
                return new Response<PriorityDto>
                {
                    Message = "There's a priority with the same name"
                };
            }

            _unitOfWork.Priorities.Delete(oldPriority);

            var newPriority = new Priority
            {
                Name = priorityForCreation.Name,
            };

            _unitOfWork.Priorities.Insert(newPriority);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<PriorityDto>
                {
                    Message = "Server Failed to Update the priority"
                };
            }

            return new Response<PriorityDto>
            {
                Data = _mapper.Map<PriorityDto>(newPriority)
            };
        }
    }
}
