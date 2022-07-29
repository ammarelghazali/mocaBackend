using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.TopUpTypeDtos.Response
{
    public class TopUpTypeDto
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string URL { get; set; }
    }
}
