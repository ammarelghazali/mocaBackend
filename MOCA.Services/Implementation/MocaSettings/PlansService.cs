using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.MocaSettings.PlanDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MOCA.Services.Implementation.MocaSettings
{
    public class PlansService : IPlansService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlansService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<Response<PlanDto>> GetByType(long? lobSpaceTypeId, long planTypeId)
        {
            Plan plan;
            if (lobSpaceTypeId == null || lobSpaceTypeId == 0)
            {
                plan = await _unitOfWork.Plans.GetByType(planTypeId);
            }
            else
            {
                plan = await _unitOfWork.Plans.GetByType((long)lobSpaceTypeId, planTypeId);
            }

            if (plan == null)
                return new Response<PlanDto> { Message = "No such a plan found" };

            var planDto = _mapper.Map<PlanDtoBase>(plan);
            planDto.LobSpaceTypeId = lobSpaceTypeId;


            return new Response<PlanDto>(new PlanDto
            {
                Id = plan.Id,
                Plan = planDto
            });
        }

        public async Task<Response<PlanDtoBase>> Add(long? lobSpaceTypeId, long planTypeId, PlanDtoBase planDto)
        {
            var planType = await _unitOfWork.PlanTypes.GetByIdAsync(planTypeId);
            if (planType == null || planType.IsDeleted)
                return new Response<PlanDtoBase> { Message = "PlanType is not exist" };


            var plan = _mapper.Map<Plan>(planDto);

            Plan existedPlan;
            if (lobSpaceTypeId == null || lobSpaceTypeId == 0)
            {
                existedPlan = await _unitOfWork.Plans.GetByType(planTypeId);
                plan.LobSpaceTypeId = null;
            }
            else
            {
                existedPlan = await _unitOfWork.Plans.GetByType((long)lobSpaceTypeId, planTypeId);
                plan.LobSpaceTypeId = lobSpaceTypeId;
            }

            if (existedPlan != null)
                return new Response<PlanDtoBase> { Message = "This plan with this type is already existed" };

            plan.TypeId = planTypeId;
            
            _unitOfWork.Plans.Insert(plan);

            var num = await _unitOfWork.SaveAsync();
            if (num != 1)
                return new Response<PlanDtoBase> {Message = "Server Error" };

            var addedPlanDto = _mapper.Map<PlanDtoBase>(plan);

            addedPlanDto.LobSpaceTypeId = lobSpaceTypeId;

            return new Response<PlanDtoBase>(addedPlanDto, "Plan added successfully");
        }


        public async Task<Response<bool>> Delete(long? lobSpaceTypeId, long planTypeId)
        {
            Plan plan;
            if (lobSpaceTypeId == null || lobSpaceTypeId == 0)
            {
                plan = await _unitOfWork.Plans.GetByType(planTypeId);
            }
            else
            {
                plan = await _unitOfWork.Plans.GetByType((long)lobSpaceTypeId, planTypeId);
            }

            if (plan == null)
                return new Response<bool>() { Message = "no such plan found" };

            _unitOfWork.Plans.Delete(plan);

            var num = await _unitOfWork.SaveAsync();
            if (num != 1)
                return new Response<bool> { Message = "Server Error" };

            return new Response<bool>(true , "Plan deleted successfully");
        }


        public async Task<Response<PlanDtoBase>> Update(long? lobSpaceTypeId, long planTypeId, PlanDtoBase planDto)
        {
            var planType = await _unitOfWork.PlanTypes.GetByIdAsync(planTypeId);
            if (planType == null || planType.IsDeleted)
                return new Response<PlanDtoBase> { Message = "PlanType is not exist" };


            var plan = _mapper.Map<Plan>(planDto);
            Plan existedPlan;
            if (lobSpaceTypeId == null || lobSpaceTypeId == 0)
            {
                existedPlan = await _unitOfWork.Plans.GetByType(planTypeId);
                plan.LobSpaceTypeId = null;
            }
            else
            {
                existedPlan = await _unitOfWork.Plans.GetByType((long)lobSpaceTypeId, planTypeId);
                plan.LobSpaceTypeId = lobSpaceTypeId;
            }

            if (existedPlan == null)
                return new Response<PlanDtoBase> { Message = "This plan is not existed" };


            _unitOfWork.Plans.Delete(existedPlan);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<PlanDtoBase> { Message = "Server Error" };

            plan.TypeId = planTypeId;
            _unitOfWork.Plans.Insert(plan);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<PlanDtoBase> { Message = "Server Error" };

            var updatedPlanDto = _mapper.Map<PlanDtoBase>(plan);
            updatedPlanDto.LobSpaceTypeId = lobSpaceTypeId;
            updatedPlanDto.TypeId = planTypeId;

            return new Response<PlanDtoBase>(updatedPlanDto, "Plan updated successfully");
        }
    }
}
