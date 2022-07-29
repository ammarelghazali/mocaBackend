using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.CaseTypesDtos.Request
{
    public class CaseTypeForCreationDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
