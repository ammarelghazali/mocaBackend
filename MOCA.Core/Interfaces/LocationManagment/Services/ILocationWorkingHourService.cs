using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface ILocationWorkingHourService
    {
        Task<Response<bool>> AddLocationWorkingHours(List<LocationWorkingHourModel> request);
        Task<Response<bool>> DeleteLocationWorkingHours(long LocationID);
        Task<Response<List<LocationWorkingHourModel>>> GetLocationWorkingHoursByLocationID(long LocationID);
    }
}
