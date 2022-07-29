using MOCA.Core.DTOs.MocaSettings.PoliciesDtos.Response;

namespace MOCA.Core.DTOs.MocaSettings.PolicyTypesDtos.Response
{
    public class PolicyTypeWithDescriptionDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public PolicyDtoMinimized Policy { get; set; }

    }
}
