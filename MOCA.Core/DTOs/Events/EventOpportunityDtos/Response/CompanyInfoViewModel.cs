
using MOCA.Core.DTOs.Events.EventOpportunityDtos.Response;

namespace MOCA.Core.DTOs.Events.EventOpportunityDtos
{
    public class CompanyInfoViewModel
    {
        public string CompanyName { get; set; }
        public ModelViewModel Industry { get; set; }
        public string Website { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }
    }
}
