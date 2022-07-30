using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.TopUpDtos.Request
{
    public class TopUpCreateionDto
    {
        [Required]
        public string TermsOfUse { get; set; }

        public long? LobSpaceTypeId { get; set; }


    }
}
