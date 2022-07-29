using MOCA.Core.DTOs.MocaSettings.WifiDtos.Request;
using MOCA.Core.DTOs.MocaSettings.WifiDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.MocaSettings.Services
{
    public interface IWifisService
    {
        Task<Response<WifiDto>> AddWifi(long? lobSpaceTypeId, WifiForCreationDto wifiForCreation);
        Task<Response<WifiDto>> UpdateWifi(long? lobSpaceTypeId, WifiForCreationDto wifiForCreation);
        Task<Response<WifiDto>> GetWifi(long? lobSpaceTypeId);
        Task<Response<bool>> DeleteWifi(long? lobSpaceTypeId);
    }
}
