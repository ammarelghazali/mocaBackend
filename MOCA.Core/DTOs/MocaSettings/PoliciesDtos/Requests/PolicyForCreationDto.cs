using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.PoliciesDtos.Requests
{
    public class PolicyForCreationDto
    {
        [Required]
        public long LobSpaceTypeId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
