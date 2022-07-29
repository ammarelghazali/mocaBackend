using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.PlanTypesDto.Request
{
    public class PlanTypeForCreationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string URL { get; set; }

    }
}
