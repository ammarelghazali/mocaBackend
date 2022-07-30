using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.MocaSettings.FaqDtos.Request;
using MOCA.Core.DTOs.MocaSettings.FaqDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MOCA.Services.Implementation.MocaSettings
{
    public class FaqService : IFaqService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FaqService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Response<FaqDto>> AddFaqAsync(FaqForCreationDto faqForCreation, long categoryId = 0)
        {
            //if (faqForCreation.LobSpaceTypeId is not null)
            //{
            //    if (!await _unitOfWork.LobSpaceTypes.LobSpaceTypeExists(
            //                                                      (long)faqForCreation.LobSpaceTypeId))
            //    {
            //        return new Response<FaqDto>
            //        {
            //            Message = "There's no such Lob Space"
            //        };
            //    }
            //}

            if (categoryId != 0)
            {
                if (!await _unitOfWork.Categories.CategoryExists(faqForCreation.LobSpaceTypeId,
                                                                 categoryId))
                {
                    return new Response<FaqDto>
                    {
                        Message = "There's no such category"
                    };
                }
            }

            var faq = _mapper.Map<Faq>(faqForCreation);

            faq.CategoryId = categoryId == 0 ? null : categoryId;
            faq.DisplayOrder = await _unitOfWork.Faqs
                              .GetMaxDisplayOrder(faqForCreation.LobSpaceTypeId, categoryId) + 1;


            _unitOfWork.Faqs.Insert(faq);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<FaqDto>
                {
                    Message = "Server failed to add faq"
                };
            }

            return new Response<FaqDto>
            {
                Message = "Faq Added Successfully",
                Data = _mapper.Map<FaqDto>(faq)
            };
        }

        public async Task<Response<IReadOnlyList<FaqDto>>> GetAllFaqsAsync(FaqsRequestSpaceIdDto getAllFaqsDto)
        {
            //if (getAllFaqsDto.LobSpaceTypeId != null)
            //{
            //    if (!await _unitOfWork.LobSpaceTypes.LobSpaceTypeExists(
            //                                                    (long)getAllFaqsDto.LobSpaceTypeId))
            //    {
            //        return new Response<IReadOnlyList<FaqDto>>
            //        {
            //            Message = "There's no such Lob Space"
            //        };
            //    }
            //}

            var faqs = await _unitOfWork.Faqs.GetAllBaseAsync(getAllFaqsDto.LobSpaceTypeId);

            return new Response<IReadOnlyList<FaqDto>>
            {
                Data = _mapper.Map<IReadOnlyList<FaqDto>>(faqs)
            };
        }

        public async Task<Response<IReadOnlyList<FaqDto>>> GetFaqsByCategoryIdAsync(FaqsRequestSpaceIdDto getFaqsDto, long categoryId = 0)
        {
            if (categoryId != 0)
            {
                if (!await _unitOfWork.Categories.CategoryExists(getFaqsDto.LobSpaceTypeId, categoryId))
                {
                    return new Response<IReadOnlyList<FaqDto>>
                    {
                        Message = "There's no such category"
                    };
                }
            }

            var faqs = await _unitOfWork.Faqs.GetAllFaqsByCategoryAsync(getFaqsDto.LobSpaceTypeId, categoryId);

            return new Response<IReadOnlyList<FaqDto>>
            {
                Data = _mapper.Map<IReadOnlyList<FaqDto>>(faqs)
            };
        }

        public async Task<Response<FaqDto>> GetSingleFaqAsync(long faqId)
        {
            var faq = await _unitOfWork.Faqs.GetByIdAsync(faqId);

            if (faq == null || faq.IsDeleted)
            {
                return new Response<FaqDto>
                {
                    Message = "There's no a faq with the id given"
                };
            }

            return new Response<FaqDto>
            {
                Data = _mapper.Map<FaqDto>(faq)
            };
        }

        public async Task<Response<FaqDto>> UpdateFaqAsync(long faqId, FaqForUpdateDto faqForUpdateDto)
        {
            var faq = await _unitOfWork.Faqs.GetFaqByIdAndLobSpaceId(faqForUpdateDto.LobSpaceTypeId,
                                                            faqId);

            if (faq == null || faq.IsDeleted)
            {
                return new Response<FaqDto>
                {
                    Message = "There's no a faq with the id given"
                };
            }

            if (faqForUpdateDto.CategoryId != null)
            {
                if (!await _unitOfWork.Categories.CategoryExists(faqForUpdateDto.LobSpaceTypeId,
                                                                 (long)faqForUpdateDto.CategoryId))
                {
                    return new Response<FaqDto>
                    {
                        Message = "There's no such category"
                    };
                }
            }

            _unitOfWork.Faqs.Delete(faq);

            var newFaq = _mapper.Map<Faq>(faqForUpdateDto);

            // If the category id changed, the faq will be added to the new category with the last display order
            if (faq.CategoryId != faqForUpdateDto.CategoryId)
            {
                bool isLast = await _unitOfWork.Faqs
                            .GetMaxDisplayOrder(faqForUpdateDto.LobSpaceTypeId, faq.CategoryId) == faq.DisplayOrder ? true : false;

                if (!isLast)
                    await _unitOfWork.Faqs.UpdateFaqsDisplayOrder(faqForUpdateDto.LobSpaceTypeId,
                                                                  faq.CategoryId, faq.DisplayOrder);

                newFaq.CategoryId = faqForUpdateDto.CategoryId;
                newFaq.DisplayOrder = await _unitOfWork.Faqs
                                     .GetMaxDisplayOrder(faqForUpdateDto.LobSpaceTypeId,
                                                         faqForUpdateDto.CategoryId) + 1;
            }
            else
            {
                newFaq.DisplayOrder = faq.DisplayOrder;
            }

            _unitOfWork.Faqs.Insert(newFaq);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<FaqDto>
                {
                    Message = "Server failed to update the faq"
                };
            }

            return new Response<FaqDto>
            {
                Message = "Faq Updated Successfully",
                Data = _mapper.Map<FaqDto>(newFaq)
            };
        }

        public async Task<Response<bool>> UpdateFaqsDisplayOrderAsync(
                                                    UpdateFaqsDisplayOrderDto updateFaqsDisplayOrderDto)
        {
            List<int> previousDisplayOrders = new List<int>();
            List<Faq> updatedFaqs = new List<Faq>();

            foreach (var category in updateFaqsDisplayOrderDto.CategoryFaqsDisplayOrderDto)
            {
                previousDisplayOrders.Clear();

                if (category.CategoryId != null)
                {
                    if (!await _unitOfWork.Categories
                              .CategoryExists(updateFaqsDisplayOrderDto.LobSpaceTypeId, (long)category.CategoryId))
                    {
                        return new Response<bool>
                        {
                            Message = "There's no such category"
                        };
                    }
                }

                foreach (var faqDisplayOrder in category.FaqsDisplayOrder)
                {
                    var faq = await _unitOfWork.Faqs.GetFaqByIdAndLobSpaceId(
                                                              updateFaqsDisplayOrderDto.LobSpaceTypeId,
                                                              faqDisplayOrder.FaqId);

                    if (faq == null || faq.IsDeleted)
                    {
                        return new Response<bool>
                        {
                            Message = "There's no a faq with the id given"
                        };
                    }

                    if (faq.CategoryId != category.CategoryId)
                        faq.CategoryId = category.CategoryId;

                    if (previousDisplayOrders.Contains(faqDisplayOrder.DisplayOrder))
                    {
                        return new Response<bool>
                        {
                            Message = $"Faqs of category " +
                                      $"{category.CategoryId} Cannot Be in The Same Order"
                        };
                    }

                    previousDisplayOrders.Add(faqDisplayOrder.DisplayOrder);

                    faq.DisplayOrder = faqDisplayOrder.DisplayOrder;

                    updatedFaqs.Add(faq);
                }

            }

            _unitOfWork.Faqs.UpdateRange(updatedFaqs);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>
                {
                    Message = "Server failed to update the faqs display order"
                };
            }

            return new Response<bool>
            {
                Data = true,
                Message = "UpdateD Faq Display Order Successfully"
            };
        }

        public async Task<Response<bool>> DeleteFaqAsync(FaqsRequestSpaceIdDto faqDto, long faqId)
        {
            var faq = await _unitOfWork.Faqs.GetFaqByIdAndLobSpaceId(faqDto.LobSpaceTypeId, faqId);

            if (faq == null || faq.IsDeleted)
            {
                return new Response<bool>
                {
                    Message = "There's no a faq with the id given"
                };
            }


            _unitOfWork.Faqs.Delete(faq);

            bool isLast = await _unitOfWork.Faqs
                         .GetMaxDisplayOrder(faqDto.LobSpaceTypeId, faq.CategoryId) == faq.DisplayOrder ? true : false;

            if (!isLast)
                await _unitOfWork.Faqs.UpdateFaqsDisplayOrder(faq.LobSpaceTypeId,
                                                              faq.CategoryId, faq.DisplayOrder);


            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>
                {
                    Message = "Server failed to delete the faq"
                };
            }

            return new Response<bool>
            {
                Data = true,
                Message = "Delete Faq Successfully"
            };
        }
    }
}
