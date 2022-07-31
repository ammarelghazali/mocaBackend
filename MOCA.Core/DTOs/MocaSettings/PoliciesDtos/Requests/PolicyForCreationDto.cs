using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.PoliciesDtos.Requests
{
    public class PolicyForCreationDto
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Lob Space Type Id cannot be zero or null")]
        public long LobSpaceTypeId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
