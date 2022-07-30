using AutoMapper;
using MMOCA.Core.DTOs.MocaSettings.CategoryDtos.Response;
using MOCA.Core;
using MOCA.Core.DTOs.MocaSettings.CategoryDtos.Request;
using MOCA.Core.DTOs.MocaSettings.CategoryDtos.Response;
using MOCA.Core.DTOs.MocaSettings.FaqDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MOCA.Services.Implementation.MocaSettings
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Response<CategoryDto>> AddCategoryAsync(CategoryForCreationDto categoryForCreation)
        {
            //if (categoryForCreation.LobSpaceTypeId is not null)
            //{
            //    if (!await _unitOfWork.LobSpaceTypes.LobSpaceTypeExists(
            //                                              (long)categoryForCreation.LobSpaceTypeId))
            //    {
            //        return new Response<CategoryDto>
            //        {
            //            StatusCode = 400,
            //            Message = "There's no such Policy Type"
            //        };
            //    }
            //}

            if (await _unitOfWork.Categories.CategoryWithSameNameExist(categoryForCreation.LobSpaceTypeId,
                                                                     categoryForCreation.Name))
            {
                return new Response<CategoryDto>
                {
                    Message = "There's Category in this Lob Space with the same name"
                };
            }


            var mappedCategory = _mapper.Map<Category>(categoryForCreation);
            mappedCategory.DisplayOrder = await _unitOfWork.Categories
                                          .GetMaxDisplayOrder(categoryForCreation.LobSpaceTypeId) + 1;

           
            _unitOfWork.Categories.Insert(mappedCategory);

            if (await _unitOfWork.SaveAsync() < 1)
                return new Response<CategoryDto>
                {
                    Message = "Server Cannot Save Resource Right now",
                };

            return new Response<CategoryDto>
            {
                Message = "Category Added Successfully",
                Data = _mapper.Map<CategoryDto>(mappedCategory)
            };
        }


        public async Task<Response<object>> GetAllCategoriesAsync(GetAllCategoriesDto getAllCategoriesDto)
        {
            //if (getAllCategoriesDto.LobSpaceTypeId is not null)
            //{
            //    if (!await _unitOfWork.LobSpaceTypes.LobSpaceTypeExists(
            //                                              (long)getAllCategoriesDto.LobSpaceTypeId))
            //    {
            //        return new Response<IReadOnlyList<CategoryDto>>
            //        {
            //            Message = "There's no such Policy Type"
            //        };
            //    }
            //}

            IList<Category> categories;

            if (getAllCategoriesDto.WithFaqs)
                categories = await _unitOfWork.Categories
                                  .GetAllCategoriesWithFaqsAsync(getAllCategoriesDto.LobSpaceTypeId);

                categories = await _unitOfWork.Categories
                             .GetAllBaseAsync(getAllCategoriesDto.LobSpaceTypeId);

            if (getAllCategoriesDto.WithFaqs)
            {
                var categoriesWithFaqs = _mapper.Map<IReadOnlyList<CategoryWithFaqDto>>(categories);

                for (int i = 0; i < categoriesWithFaqs.Count; i++)
                {
                    categoriesWithFaqs[i].Faqs = _mapper.Map<IReadOnlyList<FaqBaseDto>>(categories[i].Faqs);
                }

                if (getAllCategoriesDto.WithNonCategorizedFaqs)
                {
                    var nonCategorizedFaqs = new CategoriesWithNonCategorizedFaqs();
                    nonCategorizedFaqs.Categories = categoriesWithFaqs;

                    var faqs = await _unitOfWork.Faqs.GetNonCategorizedFaqs(getAllCategoriesDto.LobSpaceTypeId);
                    nonCategorizedFaqs.NonCategorizedFaqs = _mapper.Map<IReadOnlyList<FaqBaseDto>>(faqs);

                    return new Response<object>
                    {
                        Data = nonCategorizedFaqs
                    };
                }

                return new Response<object>
                {
                    Data = categoriesWithFaqs
                };
            }

            return new Response<object>
            {
                Data = _mapper.Map<IEnumerable<CategoryDto>>(categories)
            };
        }

        public async Task<Response<object>> GetSingleCategoryAsync(GetSingleCategoryDto getSingleCategoryDto,
                                                              long categoryId)
        {
            if (!await _unitOfWork.Categories.CategoryExists(getSingleCategoryDto.LobSpaceTypeId,
                                                            categoryId))
            {
                return new Response<object>
                {
                    Message = "there's no such category",
                };
            }

            Category category;

            if (getSingleCategoryDto.WithFaqs)
                category = await _unitOfWork.Categories
                                            .GetSingleCategoryWithFaqs(getSingleCategoryDto.LobSpaceTypeId,
                                                                      categoryId);
            else
                category = await _unitOfWork.Categories.GetByIdAsync(categoryId);

            if (getSingleCategoryDto.WithFaqs)
            {
                var categoryWithFaq = _mapper.Map<CategoryWithFaqDto>(category);
                categoryWithFaq.Faqs = _mapper.Map<List<FaqBaseDto>>(categoryWithFaq.Faqs);

                return new Response<object>
                {
                    Data = categoryWithFaq
                };
            }

            return new Response<object>
            {
                Data = _mapper.Map<CategoryDto>(category)
            };
        }

        public async Task<Response<bool>> UpdateCategoriesDisplayOrderAsync(List<UpdateCategoriesOrderDto> categoriesOrderDto,
                                                                        long? lobSpaceTypeId)
        {
            List<Category> categories = new List<Category>();
            List<int> previousDisplayOrders = new List<int>();

            foreach (var categoryOrder in categoriesOrderDto)
            {
                var category = await _unitOfWork.Categories.GetCategoryByIdAndLobSpaceId(lobSpaceTypeId,
                                                                                    categoryOrder.id);

                if (category == null || category.IsDeleted)
                {
                    return new Response<bool>
                    {
                        Message = "One or More Categories Cannot Be Found"
                    };
                }

                if (previousDisplayOrders.Contains(categoryOrder.DisplayOrder))
                {
                    return new Response<bool>
                    {
                        Message = "Categories Cannot Be in The Same Order"
                    };
                }

                previousDisplayOrders.Add(categoryOrder.DisplayOrder);

                category.DisplayOrder = categoryOrder.DisplayOrder;

                categories.Add(category);
            }

            _unitOfWork.Categories.UpdateRange(categories);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>
                {
                    Message = "Server Failed to Update the Categories Display Order"
                };
            }

            return new Response<bool>
            {
                Message = "Category Display Order Updated Successfully",
                Data = true
            };
        }

        public async Task<Response<CategoryDto>> UpdateCategoryAsync(long categoryId, CategoryForUpdateDto categoryForUpdate)
        {
            var category = await _unitOfWork.Categories.GetCategoryByIdAndLobSpaceId(categoryForUpdate
                                                                                     .LobSpaceTypeId,
                                                                                     categoryId);

            if (category == null || category.IsDeleted)
            {
                return new Response<CategoryDto>
                {
                    Message = "there's no such category"
                };
            }

            if (await _unitOfWork.Categories.CategoryWithSameNameExist(categoryForUpdate.LobSpaceTypeId,
                                                                      categoryForUpdate.Name))
            {
                return new Response<CategoryDto>
                {
                    Message = "There's Category in this Lob Space with the same name"
                };
            }

            var newCategory = _mapper.Map<Category>(category);


            _unitOfWork.Categories.Delete(category);

            newCategory.Name = categoryForUpdate.Name;
     

            _unitOfWork.Categories.Insert(newCategory);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<CategoryDto>
                {
                    Message = "Server Failed to Update the Category"
                };
            }

            var isRelatedFaqsUpdated = await _unitOfWork.Categories
                                      .UpdateRelatedFaqs(categoryForUpdate.LobSpaceTypeId,
                                                     categoryId, newCategory.Id);



            if (isRelatedFaqsUpdated)
            {
                if (await _unitOfWork.SaveAsync() < 1)
                {
                    return new Response<CategoryDto>
                    {
                        Message = "Server Cannot Update Resource Right now",
                    };
                }
            }

            return new Response<CategoryDto>
            {
                Message = "Category Updated Successfully",
                Data = _mapper.Map<CategoryDto>(newCategory)
            };
        }

        public async Task<Response<bool>> DeleteCategoryAsync(long categoryId, DeleteCategoryDto deleteCategoryDto)
        {
            if (!await _unitOfWork.Categories.CategoryExists(deleteCategoryDto.LobSpaceTypeId,
                                                             categoryId))
            {
                return new Response<bool>
                {
                    Message = "there's no such category",
                };
            }

            await _unitOfWork.Categories.DeleteCategory(deleteCategoryDto.LobSpaceTypeId,
                                                        categoryId,
                                                        deleteCategoryDto.DeleteRelatedFaqs);


            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>
                {
                    Message = "Server Failed to Delete the Category"
                };
            }

            return new Response<bool>
            {
                Data = true,
                Message = "Resource Deleted Successfully"
            };
        }
    }
}
