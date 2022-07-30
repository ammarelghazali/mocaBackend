using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.MocaSettings.TopUpDtos.Request;
using MOCA.Core.DTOs.MocaSettings.TopUpDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MOCA.Services.Implementation.MocaSettings
{
    public class TopUpsService : ITopUpsService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TopUpsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Response<TopUpDto>> Add(long topUpTypeId, TopUpCreateionDto topUpCreateionDto)
        {
            var topUpType = await _unitOfWork.TopUpTypes.GetByIdAsync(topUpTypeId);

            if (topUpType == null || topUpType.IsDeleted)
                return new Response<TopUpDto>
                {
                    Message = "No such A topup type found"
                };


            TopUp topUp;
            if (topUpCreateionDto.LobSpaceTypeId == null || topUpCreateionDto.LobSpaceTypeId == 0)
            {
                topUp = await _unitOfWork.TopUps.GetByTopUpTypeId(topUpTypeId);
                topUpCreateionDto.LobSpaceTypeId = null;
            }
            else
            {
                topUp = await _unitOfWork.TopUps.GetByTopUpTypeId(topUpTypeId, (long)topUpCreateionDto.LobSpaceTypeId);
            }

            if (topUp != null)
                return new Response<TopUpDto>
                {
                    Message = "this topup is already exist"
                };

            var topUpToBeAdded = _mapper.Map<TopUp>(topUpCreateionDto);
            topUpToBeAdded.TopUpTypeId = topUpTypeId;


            _unitOfWork.TopUps.Insert(topUpToBeAdded);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<TopUpDto>
                {
                    Message = "Server Error"
                };

            var topUpDto = _mapper.Map<TopUpDto>(topUpToBeAdded);

            return new Response<TopUpDto>(topUpDto, "Added Successfully");
        }


        public async Task<Response<TopUpDto>> GetByTopUpTypeId(long topUpTypeId, TopUpForLobSpaceTypeDto topUpForLobSpaceTypeDto)
        {
            var topUpType = await _unitOfWork.TopUpTypes.GetByIdAsync(topUpTypeId);

            if (topUpType == null || topUpType.IsDeleted)
                return new Response<TopUpDto>
                {
                    Message = "No such A topup type found"
                };


            TopUp topUp;
            if (topUpForLobSpaceTypeDto.LobSpaceTypeId == null || topUpForLobSpaceTypeDto.LobSpaceTypeId == 0)
            {
                topUp = await _unitOfWork.TopUps.GetByTopUpTypeId(topUpTypeId);
                topUpForLobSpaceTypeDto.LobSpaceTypeId = null;
            }
            else
            {
                topUp = await _unitOfWork.TopUps.GetByTopUpTypeId(topUpTypeId, (long)topUpForLobSpaceTypeDto.LobSpaceTypeId);
            }

            if (topUp == null)
                return new Response<TopUpDto> {  Message = "no such a topUp found" };

            var topUpDto = _mapper.Map<TopUpDto>(topUp);

            return new Response<TopUpDto>(topUpDto);
        }

        public async Task<Response<bool>> Delete(long topUpTypeId, TopUpForLobSpaceTypeDto topUpForLobSpaceTypeDto)
        {
            var topUpType = await _unitOfWork.TopUpTypes.GetByIdAsync(topUpTypeId);

            if (topUpType == null || topUpType.IsDeleted)
                return new Response<bool>
                {
                    Message = "No such A topup type found"
                };


            TopUp topUp;
            if (topUpForLobSpaceTypeDto.LobSpaceTypeId == null || topUpForLobSpaceTypeDto.LobSpaceTypeId == 0)
            {
                topUp = await _unitOfWork.TopUps.GetByTopUpTypeId(topUpTypeId);
            }
            else
            {
                topUp = await _unitOfWork.TopUps.GetByTopUpTypeId(topUpTypeId, (long)topUpForLobSpaceTypeDto.LobSpaceTypeId);
            }

            if (topUp == null)
                return new Response<bool>
                {
                    Message = "no such a topUp found"
                };


            _unitOfWork.TopUps.Delete(topUp);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<bool>
                {
                    Message = "Server Error"
                };

            return new Response<bool>(true, "Deleted Successfully");
        }


        public async Task<Response<TopUpDto>> Update(long topUpTypeId, TopUpForLobSpaceTypeDto topUpForLobSpaceTypeDto, UpdateTopUpDto updateTopUpDto)
        {
            var topUpType = await _unitOfWork.TopUpTypes.GetByIdAsync(topUpTypeId);

            if (topUpType == null || topUpType.IsDeleted)
                return new Response<TopUpDto>
                {
                    Message = "No such A topup type found"
                };

            TopUp topUp;
            if (topUpForLobSpaceTypeDto.LobSpaceTypeId == null || topUpForLobSpaceTypeDto.LobSpaceTypeId == 0)
            {
                topUp = await _unitOfWork.TopUps.GetByTopUpTypeId(topUpTypeId);
                topUpForLobSpaceTypeDto.LobSpaceTypeId = null;
            }
            else
            {
                topUp = await _unitOfWork.TopUps.GetByTopUpTypeId(topUpTypeId,
                (long)topUpForLobSpaceTypeDto.LobSpaceTypeId);
            }
            if (topUp == null)
                return new Response<TopUpDto>
                {
                    Message = "Thid TopUp Not Found"
                };

            _unitOfWork.TopUps.Delete(topUp);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<TopUpDto>
                {
                    Message = "Server Error"
                };

            var topUpToBeAdded = _mapper.Map<TopUp>(updateTopUpDto);
            topUpToBeAdded.TopUpTypeId = topUpTypeId;
            topUpToBeAdded.LobSpaceTypeId = topUpForLobSpaceTypeDto.LobSpaceTypeId;

            _unitOfWork.TopUps.Insert(topUpToBeAdded);


            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<TopUpDto>
                {
                    Message = "Server Error"
                };


            var topUpDto = _mapper.Map<TopUpDto>(topUpToBeAdded);

            return new Response<TopUpDto>(topUpDto, "Updated Successfully");
        }
    }
}
