using MOCA.Core.DTOs.Events.EventsOpportunitiesDtos.Request;
using MOCA.Core.DTOs.Events.EventsOpportunitiesDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.Events.Services
{
    public interface IEventsOpportunitiesService
    {
        Task<Response<GetEmailTempleteEventOpportunitylViewModelDto>> GetEmailTempletType(GetEmailTempleteEventOpportunityDto request);
    }
}
