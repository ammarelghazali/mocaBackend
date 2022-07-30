using MOCA.Core.DTOs.LocationManagment.District;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface IDistrictService
    {
        Task<Response<long>> AddDistrict(DistrictModel request);
        Task<Response<bool>> UpdateDistrict(DistrictModel request);
        Task<Response<DistrictModel>> GetDistrictByID(long Id);
        Task<PagedResponse<List<DistrictModel>>> GetAllDistrictsWithPagination(RequestParameter filter);
        Task<Response<List<DistrictModel>>> GetAllDistrictsWithoutPagination();
        Task<Response<List<DistrictModel>>> GetAllDistrictsByCityID(long CityID);
        Task<Response<bool>> DeleteDistrict(long Id);
    }
}
