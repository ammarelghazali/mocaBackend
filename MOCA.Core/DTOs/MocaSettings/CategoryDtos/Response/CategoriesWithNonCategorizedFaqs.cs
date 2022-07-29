using MMOCA.Core.DTOs.MocaSettings.CategoryDtos.Response;
using MocaContentBack.Core.Dtos.FaqDtos.ResponsesDtos;

namespace MOCA.Core.DTOs.MocaSettings.CategoryDtos.Response
{
    public class CategoriesWithNonCategorizedFaqs
    {
        public List<CategoryWithFaqDto> Categories { get; set; }
        public List<FaqBaseDto> NonCategorizedFaqs { get; set; }
    }
}
