namespace MOCA.Core.DTOs.MocaSettings.FaqDtos.Response
{
    public class FaqWithCategoryDto
    {
        public long? CategoryId { get; set; }
        List<FaqBaseDto> Faqs { get; set; }
    }
}
