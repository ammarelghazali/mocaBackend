using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.FaqDtos.Request
{
    public class FaqForCreationDto
    {
        [Range(1, long.MaxValue, ErrorMessage = "Space Id Cannot Be 0")]
        public long? LobSpaceTypeId { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string Answer { get; set; }
    }
}
