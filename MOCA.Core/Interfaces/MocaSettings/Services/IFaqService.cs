using MOCA.Core.DTOs.MocaSettings.FaqDtos.Request;
using MOCA.Core.DTOs.MocaSettings.FaqDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.MocaSettings.Services
{
    public interface IFaqService
    {
        Task<Response<FaqDto>> AddFaqAsync(FaqForCreationDto faqForCreation, long categoryId = 0);
        Task<Response<IReadOnlyList<FaqDto>>> GetAllFaqsAsync(FaqsRequestSpaceIdDto getAllFaqsDto);
        Task<Response<IReadOnlyList<FaqDto>>> GetFaqsByCategoryIdAsync(FaqsRequestSpaceIdDto getFaqsDto, long categoryId = 0);
        Task<Response<FaqDto>> GetSingleFaqAsync(long faqId);
        Task<Response<FaqDto>> UpdateFaqAsync(long faqId, FaqForUpdateDto faqForUpdateDto);
        Task<Response<bool>> UpdateFaqsDisplayOrderAsync(UpdateFaqsDisplayOrderDto updateFaqsDisplayOrderDto);
        Task<Response<bool>> DeleteFaqAsync(FaqsRequestSpaceIdDto faqDto, long faqId);
    }
}
