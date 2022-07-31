using MOCA.Core.DTOs.MocaSettings.CategoryDtos.Request;
using MOCA.Core.DTOs.MocaSettings.CategoryDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.MocaSettings.Services
{
    public interface ICategoryService
    {
        Task<Response<CategoryDto>> AddCategoryAsync(CategoryForCreationDto categoryForCreation);
        Task<Response<object>> GetAllCategoriesAsync(GetAllCategoriesDto getAllCategoriesDto);
        Task<Response<object>> GetSingleCategoryAsync(GetSingleCategoryDto getSingleCategoryDto, long categoryId);
        Task<Response<CategoryDto>> UpdateCategoryAsync(long categoryId, CategoryForUpdateDto categoryForUpdate);
        Task<Response<bool>> DeleteCategoryAsync(long categoryId, DeleteCategoryDto deleteCategoryDto);
        Task<Response<bool>> UpdateCategoriesDisplayOrderAsync(List<UpdateCategoriesOrderDto> categoriesOrderDto, long? lobSpaceTypeId);
    }
}
