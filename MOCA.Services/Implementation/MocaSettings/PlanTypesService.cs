using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.MocaSettings.PlanTypesDto.Request;
using MOCA.Core.DTOs.MocaSettings.PlanTypesDto.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MOCA.Services.Implementation.MocaSettings
{
    public class PlanTypesService : IPlanTypesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlanTypesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<IList<PlanTypeDto>>> GetAll()
        {
            var planTypes = await _unitOfWork.PlanTypes.GetAllTypes();
            var allPlanTypes = _mapper.Map<IList<PlanType>, IList<PlanTypeDto>>(planTypes);

            if (planTypes == null)
                return new Response<IList<PlanTypeDto>> { Message = "No plan types found" };

            return new Response<IList<PlanTypeDto>> { Data = allPlanTypes };

        }


        public async Task<Response<PlanTypeManipulationResponse>> Add(PlanTypeForCreationDto planTypeDto)
        {
            var planType = _mapper.Map<PlanType>(planTypeDto);

            var planTypeByName = await _unitOfWork.PlanTypes.GetByName(planType.Name);
            if (planTypeByName != null)
                return new Response<PlanTypeManipulationResponse> { Message = "this type is already exist" };

            _unitOfWork.PlanTypes.Insert(planType);

            var num = await _unitOfWork.SaveAsync();
            if (num != 1)
                return new Response<PlanTypeManipulationResponse> { Message = "Server Error" };

            return new Response<PlanTypeManipulationResponse>
            {
                Message = "Added Successfully",
                Data = new PlanTypeManipulationResponse
                {
                    Id = planType.Id,
                    Name = planType.Name,
                    URL = planType.URL,
                }
            };

        }



        public async Task<Response<PlanTypeManipulationResponse>> Update(long id, PlanTypeForCreationDto planTypeDto)
        {
            var planType = await _unitOfWork.PlanTypes.GetByIdAsync(id);
            if (planType == null || planType.IsDeleted)
                return new Response<PlanTypeManipulationResponse> { Message = "no such a type found" };

            var planTypeByName = await _unitOfWork.PlanTypes.GetByName(planTypeDto.Name);

            if (planTypeByName != null)
                return new Response<PlanTypeManipulationResponse> { Message = "this type name is already exist" };

            _unitOfWork.PlanTypes.Delete(planType);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<PlanTypeManipulationResponse> { Message = "Server Error" };


            var planTypeTobeAdded = _mapper.Map<PlanType>(planTypeDto);

            _unitOfWork.PlanTypes.Insert(planTypeTobeAdded);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<PlanTypeManipulationResponse> { Message = "Server Error" };



            var plans = await _unitOfWork.Plans.GetAllPlansByTypeId(id);
            foreach (var plan in plans)
                plan.TypeId = planTypeTobeAdded.Id;

            _unitOfWork.Plans.UpdateRange(plans);

            var num = await _unitOfWork.SaveAsync();
            if (num != plans.Count())
                return new Response<PlanTypeManipulationResponse> { Message = "Server Error" };


            return new Response<PlanTypeManipulationResponse>
            {
                Message = "Updated Successfully",
                Data = new PlanTypeManipulationResponse
                {
                    Id = planTypeTobeAdded.Id,
                    Name = planTypeTobeAdded.Name,
                    URL = planTypeTobeAdded.URL,
                }
            };

        }

        public async Task<Response<bool>> Delete(long id)
        {
            var planType = await _unitOfWork.PlanTypes.GetByIdAsync(id);

            if (planType == null || planType.IsDeleted)
                return new Response<bool> { Message = "no such type found" };

            _unitOfWork.PlanTypes.Delete(planType);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<bool> { Message = "Server Error" };

            // delete all related plans with this type id
            var plans = await _unitOfWork.Plans.GetAllPlansByTypeId(id);

            _unitOfWork.Plans.DeleteRange(plans);

            var num = await _unitOfWork.SaveAsync();
            if (num != plans.Count())
                return new Response<bool> { Message = "Server Error" };

            return new Response<bool> { Message = "Deleted Successfully", Data = true };
        }
    }
}
