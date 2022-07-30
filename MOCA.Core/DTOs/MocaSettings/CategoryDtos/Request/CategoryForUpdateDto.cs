using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.CategoryDtos.Request
{
    public class CategoryForUpdateDto
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Space Id Cannot Be 0")]
        public long LobSpaceTypeId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
