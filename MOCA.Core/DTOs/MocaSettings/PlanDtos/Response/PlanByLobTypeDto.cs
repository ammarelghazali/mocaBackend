using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.PlanDtos.Response
{
    public class PlanByLobTypeDto
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Lob Space Type Id cannot be zero or null")]
        public long LobSpaceTypeId { get; set; }
    }
}
