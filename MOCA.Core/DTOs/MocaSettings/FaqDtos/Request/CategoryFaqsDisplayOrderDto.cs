using System.ComponentModel.DataAnnotations;


namespace MOCA.Core.DTOs.MocaSettings.FaqDtos.Request
{
    public class CategoryFaqsDisplayOrderDto
    {
        public long? CategoryId { get; set; } = null;
        [Required]
        public List<FaqDisplayOrderDto> FaqsDisplayOrder { get; set; }
    }
}
