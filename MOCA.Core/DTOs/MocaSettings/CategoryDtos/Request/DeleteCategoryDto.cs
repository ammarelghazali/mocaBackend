using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.CategoryDtos.Request
{
    public class DeleteCategoryDto
    {
        [Range(1, long.MaxValue, ErrorMessage = "Space Id Cannot Be 0")]
        public long? LobSpaceTypeId { get; set; }
        public bool DeleteRelatedFaqs { get; set; } = true;
    }
}
