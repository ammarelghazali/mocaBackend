using MOCA.Core.DTOs.MocaSettings.TopUpTypeDtos.Request;
using MOCA.Core.DTOs.MocaSettings.TopUpTypeDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.MocaSettings.Services
{
    public interface ITopUpTypesService
    {
        Task<Response<IReadOnlyList<TopUpTypeDto>>> GetAllTopUpTypes();
        Task<Response<TopUpTypeDto>> GetTopUpTypeById(long id);
        Task<Response<TopUpTypeDto>> GetTopUpTypeByName(string name);
        Task<Response<TopUpTypeDto>> AddTopUpType(AddTopUpTypeDto addTopUpTypeDto);
        Task<Response<bool>> DeleteTopUpType(long id);
        Task<Response<TopUpTypeDto>> UpdateTopUpType(long id, AddTopUpTypeDto topUpTypeDto);
    }
}
