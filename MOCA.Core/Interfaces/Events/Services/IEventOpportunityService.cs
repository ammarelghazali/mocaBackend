using MOCA.Core.DTOs.Events.EventOpportunityDtos.Request;
using MOCA.Core.DTOs.Events.EventOpportunityDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.Events.Services
{
    public interface IEventOpportunityService
    {
        Task<Response<long>> CreateNewOpportunity(cmdCreateNewEventOpportunityParameter request);
        Task<Response<bool>> DeleteOpportunity(cmdDeleteEventOpportunityParameter request);
        Task<Response<bool>> UpdateOpportunity(cmdUpdateEventOpportunityParameter request);
        Task<Response<EventOpportunityDetailsViewModel>> GetOpportunityDetailsByEventOpportunityID(
                                                                                cmdGetEventOpportunityDetailsParameter request);

        Task<Response<IList<EventOpportunityDetails_SendViewModel>>> GetOpportunityDetailsWithoutPagination(long locationTypeId);

        Task<PagedResponse<IList<EventOpportunityDetails_SendViewModel>>> FilterWithPagination(cmdFilterEventOpportunityDetailsWithPagination_Query request);

        Task<Response<bool>> SendEmails(cmdPostSendEmailParameter request);

        Task<Response<cmdGetDetailedEventOpportunityViewModel>> GetEventOpportunityDetails(
                                                            cmdGetDetailedEventOpportunityParameter request);

        Task<Response<bool>> SaveEventOpportunityDetails(cmdPostEventOpportunityStageReportParameter request);
    }
}
