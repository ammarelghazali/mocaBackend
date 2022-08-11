

using MOCA.Core.DTOs;
using MOCA.Core.DTOs.LocationManagment.Amenity;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface IAmenityService
    {
        Task<Response<long>> AddAmenity(AmenityModel request);
        Task<Response<List<AmenityModel>>> AddListOfAmenity(List<AmenityModel> request);
        Task<Response<bool>> UpdateAmenity(AmenityModel request);

        Task<PagedResponse<List<AmenityModel>>> GetAllAmenityPaginated(RequestParameter filter);

        Task<Response<List<AmenityModel>>> GetAmenityWithoutPagination();
        Task<Response<AmenityModel>> GetAmenityById(long Id);
        Task<Response<bool>> DeleteAmenity(long Id);
    }
}
