using Compolitan.Core.DTOs;
using MOCA.Core.DTOs.LocationManagment.LocationType;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface ILocationTypeService
    {
        Task<Response<long>> AddLocationType(LocationTypeModel request);
        Task<Response<bool>> UpdateLocationType(LocationTypeModel request);
        Task<Response<LocationTypeModel>> GetLocationTypeByID(long Id);
        Task<PagedResponse<List<LocationTypeModel>>> GetAllLocationTypesWithPagination(RequestParameter filter);
        Task<Response<List<LocationTypeModel>>> GetAllLocationTypesWithoutPagination();
        Task<Response<bool>> DeleteLocationType(long Id);
    }
}
