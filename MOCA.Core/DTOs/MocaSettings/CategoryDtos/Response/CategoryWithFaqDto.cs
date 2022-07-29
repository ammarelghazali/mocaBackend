using MOCA.Core.DTOs.MocaSettings.FaqDtos.Response;

namespace MMOCA.Core.DTOs.MocaSettings.CategoryDtos.Response
{
    public class CategoryWithFaqDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public List<FaqBaseDto> Faqs { get; set; }
    }
}
