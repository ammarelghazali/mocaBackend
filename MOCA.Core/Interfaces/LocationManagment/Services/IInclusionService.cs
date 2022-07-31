using MOCA.Core.DTOs.LocationManagment.Inclusion;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface IInclusionService
    {
        Task<Response<long>> AddInclusion(InclusionModel request);
        Task<Response<bool>> UpdateInclusion(InclusionModel request);
        Task<Response<InclusionModel>> GetInclusionByID(long Id);
        Task<PagedResponse<List<InclusionModel>>> GetAllInclusionsWithPagination(RequestParameter filter);
        Task<Response<List<InclusionModel>>> GetAllInclusionsWithoutPagination();
        Task<Response<bool>> DeleteInclusion(long Id);
    }
}
