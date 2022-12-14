using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.FaqDtos.Request
{
    public class FaqsRequestSpaceIdDto
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Space Id Cannot Be 0")]
        public long LobSpaceTypeId { get; set; }
    }
}
