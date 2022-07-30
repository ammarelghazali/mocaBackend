using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.TopUpDtos.Request
{
    public class UpdateTopUpDto
    {
        [Required]
        public string TermsOfUse { get; set; }
    }
}
