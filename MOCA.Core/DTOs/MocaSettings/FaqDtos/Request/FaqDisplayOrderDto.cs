using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.FaqDtos.Request
{
    public class FaqDisplayOrderDto
    {
        [Required]
        public long FaqId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Faqs Display Order Cannot Be 0")]
        public int DisplayOrder { get; set; }
    }
}
