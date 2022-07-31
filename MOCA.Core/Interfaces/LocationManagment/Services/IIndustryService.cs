using MOCA.Core.DTOs.LocationManagment.Industry;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface IIndustryService
    {
        Task<Response<long>> AddIndustry(IndustryModel request);
        Task<Response<bool>> UpdateIndustry(IndustryModel request);
        Task<Response<IndustryModel>> GetIndustryByID(long Id);
        Task<PagedResponse<List<IndustryModel>>> GetAllIndustriesWithPagination(RequestParameter filter);
        Task<Response<List<IndustryModel>>> GetAllIndustriesWithoutPagination();
        Task<Response<bool>> DeleteIndustry(long Id);
    }
}
