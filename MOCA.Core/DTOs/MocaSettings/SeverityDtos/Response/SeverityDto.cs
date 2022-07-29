using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.SeverityDtos.Response
{
    public class SeverityDto
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
