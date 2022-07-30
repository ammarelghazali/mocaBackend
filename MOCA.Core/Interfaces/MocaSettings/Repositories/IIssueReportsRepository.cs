using MOCA.Core.DTOs.MocaSettings.IssueReportDtos.Request;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IIssueReportsRepository : IRepository<IssueReport>
    {
        Task<long?> GetMaxReportId();
        Task<IList<IssueCaseStage>> GetCaseStages(long IssueReportId);
        Task<IList<IssueReport>> GetReportsWithPagination(long? lobSpaceTypeId,
                                                          IssueReportsResourceParameters resourceParameters);
        Task<bool> IssueReportExists(long IssueReportId);

        Task<List<IssueReport>> GetAllIssueReporstWithStatusId(long statusId);
        Task<List<IssueReport>> GetAllIssueReporstWithSeveritysId(long severityId);
        Task<IssueReport> GetIssueReportById(long id);
        Task<IssueCaseStage> AddIssueCaseStage(IssueCaseStage issueCaseStage);
        Task<IssueReport> GetIssueById(long IssueReportId);
    }
}
