using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.LobSpaceTypeDtos.Request
{
    public class LobSpaceTypeCreationDto
    {
        [Required, MaxLength(500)]
        public string Name { get; set; }
    }
}
