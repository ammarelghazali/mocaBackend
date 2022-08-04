using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface ILocationFileService
    {
        Task<Response<bool>> AddLocationFiles(List<LocationFileModel> request);
        Task<Response<bool>> DeleteLocationFiles(long LocationID);
        Task<Response<List<LocationFileModel>>> GetLocationFilesByLocationID(long LocationID);
    }
}
