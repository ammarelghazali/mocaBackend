using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.PriorityDtos.Request
{
    public class PriorityForCreationDto
    {
        [Required]
        public string Name { get; set; }
    }
}
