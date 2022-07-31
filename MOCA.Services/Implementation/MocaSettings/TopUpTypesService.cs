using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.MocaSettings.TopUpTypeDtos.Request;
using MOCA.Core.DTOs.MocaSettings.TopUpTypeDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MOCA.Services.Implementation.MocaSettings
{
    public class TopUpTypesService : ITopUpTypesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public TopUpTypesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<TopUpTypeDto>>> GetAllTopUpTypes()
        {
            var topUpTypes = await _unitOfWork.TopUpTypes.GetAllNotDeletedAsync();
            var allTopUpTypes = _mapper.Map<IList<TopUpType>, IReadOnlyList<TopUpTypeDto>>(topUpTypes);

            if (allTopUpTypes == null)
                return new Response<IReadOnlyList<TopUpTypeDto>>
                {
                    Message = "No Types Found"
                };

            return new Response<IReadOnlyList<TopUpTypeDto>>(allTopUpTypes);
        }

        public async Task<Response<TopUpTypeDto>> GetTopUpTypeById(long id)
        {
            var topUpType = await _unitOfWork.TopUpTypes.GetByIdAsync(id);

            if (topUpType == null || topUpType.IsDeleted)
                return new Response<TopUpTypeDto>
                {
                    Message = "no such a topUpType found"
                };

            var topUpTypeDto = _mapper.Map<TopUpTypeDto>(topUpType);

            return new Response<TopUpTypeDto>(topUpTypeDto);
        }

        public async Task<Response<TopUpTypeDto>> GetTopUpTypeByName(string name)
        {
            var topUpType = await _unitOfWork.TopUpTypes.GetByName(name);

            if (topUpType == null)
                return new Response<TopUpTypeDto> { Message = "no such a topUpType found" };

            var topUpTypeDto = _mapper.Map<TopUpTypeDto>(topUpType);

            return new Response<TopUpTypeDto>(topUpTypeDto);
        }

        public async Task<Response<TopUpTypeDto>> AddTopUpType(AddTopUpTypeDto addTopUpTypeDto)
        {
            var topUpType = await _unitOfWork.TopUpTypes.GetByName(addTopUpTypeDto.Name);

            if (topUpType != null)
                return new Response<TopUpTypeDto>
                {
                    Message = "topUpType is already exist"
                };

            TopUpType topUpTypeTobeAdded = _mapper.Map<TopUpType>(addTopUpTypeDto);

            _unitOfWork.TopUpTypes.Insert(topUpTypeTobeAdded);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<TopUpTypeDto>
                {
                    Message = "Server Error"
                };

            return new Response<TopUpTypeDto>(_mapper.Map<TopUpTypeDto>(topUpTypeTobeAdded), "Added Successfully");
        }

        public async Task<Response<TopUpTypeDto>> UpdateTopUpType(long id, AddTopUpTypeDto topUpTypeDto)
        {
            var topUpType = await _unitOfWork.TopUpTypes.GetByIdAsync(id);

            if (topUpType == null || topUpType.IsDeleted)
                return new Response<TopUpTypeDto>
                {
                    Message = "No such a type found"
                };

            var topUpTypeByName = await _unitOfWork.TopUpTypes.GetByName(topUpTypeDto.Name);

            if (topUpTypeByName != null)
                return new Response<TopUpTypeDto>
                {
                    Message = "this name type is already exist"
                };


            _unitOfWork.TopUpTypes.Delete(topUpType);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<TopUpTypeDto>
                {
                    Message = "Server Error"
                };


            TopUpType topUpTypeTobeAdded = _mapper.Map<TopUpType>(topUpTypeDto);

            _unitOfWork.TopUpTypes.Insert(topUpTypeTobeAdded);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<TopUpTypeDto>
                {
                    Message = "Server Error"
                };

            var updated = await _unitOfWork.TopUpTypes.UpdateRelatedTopUps(topUpType.Id, topUpTypeTobeAdded.Id);

            if (updated)
            {
                if (await _unitOfWork.SaveAsync() < 1)
                    return new Response<TopUpTypeDto>
                    {
                        Message = "Server Error"
                    };
            }

            return new Response<TopUpTypeDto>(_mapper.Map<TopUpTypeDto>(topUpTypeTobeAdded), "Updated Successfully");
        }

        public async Task<Response<bool>> DeleteTopUpType(long id)
        {
            var topUpType = await _unitOfWork.TopUpTypes.GetByIdAsync(id);

            if (topUpType == null || topUpType.IsDeleted)
                return new Response<bool>
                {
                    Message = "No such a type found"
                };

            _unitOfWork.TopUpTypes.Delete(topUpType);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<bool>
                {
                    Message = "Server Error"
                };

            var deleted = await _unitOfWork.TopUpTypes.DeleteRelatedTopUps(id);

            if (deleted)
            {
                if (await _unitOfWork.SaveAsync() < 1)
                    return new Response<bool>
                    {
                        Message = "Server Error"
                    };
            }

            return new Response<bool>(true, "Deleted Successfully");
        }
    }
}
