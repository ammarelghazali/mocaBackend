using MMOCA.Core.DTOs.MocaSettings.CategoryDtos.Response;
using MOCA.Core.DTOs.MocaSettings.FaqDtos.Response;

namespace MOCA.Core.DTOs.MocaSettings.CategoryDtos.Response
{
    public class CategoriesWithNonCategorizedFaqs
    {
        public IReadOnlyList<CategoryWithFaqDto> Categories { get; set; }
        public IReadOnlyList<FaqBaseDto> NonCategorizedFaqs { get; set; }
    }
}
