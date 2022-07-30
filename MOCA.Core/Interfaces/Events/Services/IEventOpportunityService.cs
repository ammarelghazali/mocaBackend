using MOCA.Core.DTOs.Events.EventOpportunityDtos.Request;
using MOCA.Core.DTOs.Events.EventOpportunityDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.Events.Services
{
    public interface IEventOpportunityService
    {
        Task<Response<long>> CreateNewOpportunity(cmd_Create_NewEventOpportunity_Parameter request);
        Task<Response<bool>> DeleteOpportunity(cmd_Delete_EventOpportunity_Parameter request);
        Task<Response<bool>> UpdateOpportunity(cmd_Update_EventOpportunity_Parameter request);
        Task<Response<EventOpportunityDetails_ViewModel>> GetOpportunityDetailsByEventOpportunityID(
                                                                                cmd_Get_EventOpportunityDetails_Parameter request);

        Task<Response<IList<EventOpportunityDetails_Send_ViewModel>>> GetOpportunityDetailsWithoutPagination(long locationTypeId);

        Task<PagedResponse<IList<EventOpportunityDetails_Send_ViewModel>>> FilterWithPagination(cmd_Filter_EventOpportunityDetails_WithPagination_Query request);

        Task<Response<bool>> SendEmails(cmd_Post_SendEmail_Parameter request);

        Task<Response<cmd_Get_DetailedEventOpportunity_ViewModel>> GetEventOpportunityDetails(
                                                            cmd_Get_DetailedEventOpportunity_Parameter request);

        Task<Response<bool>> SaveEventOpportunityDetails(cmd_Post_EventOpportunityStageReport_Parameter request);
    }
}
