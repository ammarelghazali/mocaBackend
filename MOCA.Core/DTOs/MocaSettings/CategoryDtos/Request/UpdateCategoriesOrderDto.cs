using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.CategoryDtos.Request
{
    public class UpdateCategoriesOrderDto
    {
        [Required]
        public long id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Category Display Order Cannot Be 0")]
        public int DisplayOrder { get; set; }
    }
}
