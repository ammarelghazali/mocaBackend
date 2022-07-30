using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.CategoryDtos.Request
{
    public class GetSingleCategoryDto
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Space Id Cannot Be 0")]
        public long LobSpaceTypeId { get; set; }
        public bool WithFaqs { get; set; } = true;
    }
}
