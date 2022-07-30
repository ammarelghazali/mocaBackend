using MOCA.Core.DTOs.MocaSettings.IssueReportDtos.Request;
using MOCA.Core.DTOs.MocaSettings.IssueReportDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.MocaSettings.Services
{
    public interface IIssueReportService
    {
        Task<Response<InfoForNewIssueReportDto>> GetInfoForNewIssueReportAsync(Guid adminId);
        Task<Response<bool>> AddIssueReportAsync(IssueReportForCreationDto issueReportForCreationDto);
        Task<Response<bool>> DeleteIssueReportAsync(long issueReportId);
        Task<Response<IReadOnlyList<IssueCaseStagesDto>>> GetIssueReportCaseStages(long issueReportId);
        Task<PagedResponse<List<IssueReportDto>>> GetPaginatedIssueReportsAsync(long? lobSpaceTypeId,
                                                        IssueReportsResourceParameters resourceParameters);
        Task<Response<IssueReportDto>> GetSingleIssueReportAsync(long issueReportId);
        Task<Response<bool>> UpdateIssueReportAsync(long id, UpdateIssueReportDto updateIssueReportDto);

    }
}
