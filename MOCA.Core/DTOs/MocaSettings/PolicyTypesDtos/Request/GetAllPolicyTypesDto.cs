using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.PolicyTypesDtos.Request
{
    public class GetAllPolicyTypesDto
    {
        public long? LobSpaceTypeId { get; set; }
        [Required]
        public bool WithRelatedDescription { get; set; } = true;
        public string URL { get; set; }

    }
}
