using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.FaqDtos.Request
{
    public class FaqsRequestSpaceIdDto
    {
        [Range(1, long.MaxValue, ErrorMessage = "Space Id Cannot Be 0")]
        public long? LobSpaceTypeId { get; set; }
    }
}
