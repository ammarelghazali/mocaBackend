using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.StatusDto.Request
{
    public class StatusForCreationDto
    {
        [Required]
        public string Name { get; set; }
    }
}
