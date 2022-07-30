using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.WifiDtos.Request
{
    public class WifiForCreationDto
    {
        [Required]
        public string Description { get; set; }
    }
}
