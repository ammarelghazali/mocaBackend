using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.TopUpTypeDtos.Request
{
    public class AddTopUpTypeDto
    {
        [Required, MaxLength(500)]
        public string Name { get; set; }
        [Required]
        public string URL { get; set; }
    }
}
