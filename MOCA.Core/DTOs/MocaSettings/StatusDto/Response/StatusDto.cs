using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.StatusDto.Response
{
    public class StatusDto
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
