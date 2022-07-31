using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface ILocationImageService
    {
        Task<Response<bool>> AddLocationImages(List<LocationImageModel> request);
        Task<Response<bool>> DeleteLocationImages(long LocationID);
        Task<Response<List<LocationImageModel>>> GetLocationImagesByLocationID(long LocationID);
    }
}
