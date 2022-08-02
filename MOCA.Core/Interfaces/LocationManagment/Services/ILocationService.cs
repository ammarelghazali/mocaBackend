using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface ILocationService
    {
        Task<Response<long>> AddLocation(LocationModel request);

        Task<Response<long>> UpdateLocation(LocationModel request);

        Task<Response<bool>> DeleteLocation(long LocationId);

        Task<Response<LocationDropDown>> GetAllForDropDown();

        Task<Response<LocationDetailsModel>> GetLocationByID(long Id);

        Task<PagedResponse<List<LocationModel>>> GetAllLocationWithPagination(RequestParameter filter);

        Task<Response<List<LocationModel>>> GetAllLocationWithoutPagination();

        Task<Response<bool>> UpdateLocationPublishStatus(long LocationId);

        Task<Response<List<LocationGetAllModel>>> GetAllUnpublishedLocation();

        Task<Response<List<LocationGetAllModel>>> GetAllPublishedAndUnpublishedLocation(RequestParameter filter);
    }
}
