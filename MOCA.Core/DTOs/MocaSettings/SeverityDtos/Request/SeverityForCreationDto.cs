using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.SeverityDtos.Request
{
    public class SeverityForCreationDto
    {
        [Required]
        public string Name { get; set; }
    }
}
