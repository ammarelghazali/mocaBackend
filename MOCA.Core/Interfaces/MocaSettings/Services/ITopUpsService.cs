using MOCA.Core.DTOs.MocaSettings.TopUpDtos.Request;
using MOCA.Core.DTOs.MocaSettings.TopUpDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.MocaSettings.Services
{
    public interface ITopUpsService
    {
        Task<Response<TopUpDto>> Add(long topUpTypeId, TopUpCreateionDto topUpCreateionDto);
        Task<Response<TopUpDto>> GetByTopUpTypeId(long id, TopUpForLobSpaceTypeDto topUpForLobSpaceTypeDto);
        Task<Response<bool>> Delete(long id, TopUpForLobSpaceTypeDto topUpForLobSpaceTypeDto);
        Task<Response<TopUpDto>> Update(long topUpTypeId, TopUpForLobSpaceTypeDto topUpForLobSpaceTypeDto, UpdateTopUpDto updateTopUpDto);
    }
}
