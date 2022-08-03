using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.TopUpDtos.Request
{
    public class TopUpForLobSpaceTypeDto
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Lob Space Type Id cannot be zero or null")]
        public long LobSpaceTypeId { get; set; }
    }
}
