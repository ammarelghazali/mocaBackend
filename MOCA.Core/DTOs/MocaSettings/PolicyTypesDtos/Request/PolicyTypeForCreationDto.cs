using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.PolicyTypesDtos.Request
{
    public class PolicyTypeForCreationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string URL { get; set; }
    }
}
