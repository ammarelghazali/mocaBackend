using MOCA.Core.DTOs.LocationManagment.Building;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface IBuildingService
    {
        Task<Response<long>> AddBuilding(BuildingModel request);

    }
}
