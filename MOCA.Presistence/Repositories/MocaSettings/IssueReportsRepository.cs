using Microsoft.EntityFrameworkCore;
using MOCA.Core.DTOs.MocaSettings.IssueReportDtos.Request;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.MocaSettings
{
    public class IssueReportsRepository : GenericRepository<IssueReport>, IIssueReportsRepository
    {
        private readonly ApplicationDbContext _context;
        public IssueReportsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<long?> GetMaxReportId()
        {
            if (!await _context.IssueReports.AnyAsync())
            {
                return 0;
            }

            return await _context.IssueReports.MaxAsync(x => x.Id);
        }

        public async Task<IssueReport> GetIssueReportById(long id)
        {
            return await _context.IssueReports.Where(x => x.Id == id && x.IsDeleted == false)
                                              .Include(x => x.Status)
                                              .Include(x => x.Priority)
                                              .Include(x => x.Severity)
                                              .Include(x => x.CaseType)
                                              .FirstOrDefaultAsync();
        }



        public async Task<IList<IssueCaseStage>> GetCaseStages(long IssueReportId)
        {
            return await _context.IssueCaseStages.Where(c => c.IsDeleted != true && c.Id == IssueReportId)
                                                 .Include(c => c.IssueReport)
                                                 .ThenInclude(i => i.Status)
                                                 .ToListAsync();
        }

        public async Task<IList<IssueReport>> GetReportsWithPagination(long? lobSpaceTypeId,
                                                                 IssueReportsResourceParameters resourceParameters)
        {


            var issueReports = _context.IssueReports.Where(i => i.LobSpaceTypeId == lobSpaceTypeId &&
                                                                i.IsDeleted != true)
                                                    .Include(i => i.CaseType)
                                                    .Include(i => i.Severity)
                                                    .Include(i => i.Status)
                                                    .Include(i => i.Priority)
                                                    as IQueryable<IssueReport>;

            if (resourceParameters.Id is not null && resourceParameters.Id != 0)
            {
                issueReports = issueReports.Where(i => i.Id == resourceParameters.Id);
            }

            if (resourceParameters.SubmissionDate is not null)
            {
                var date = DateTime.Parse(resourceParameters.SubmissionDate.Trim());
                issueReports = issueReports.Where(i => i.SubmissionDate.Date == date.Date);
            }

            if (resourceParameters.ReportedBy is not null && resourceParameters.ReportedBy != string.Empty)
            {
                //issueReports = issueReports.Where(i => i.ReportedById == resourceParameters.ReportedBy);
            }


            if (resourceParameters.Owner is not null && resourceParameters.Owner != string.Empty)
            {
                issueReports = issueReports.Where(i => i.OwnerId == resourceParameters.Owner);
            }

            if (resourceParameters.Location is not null)
            {
                issueReports = issueReports.Where(i => i.LocationId == resourceParameters.Location);
            }

            if (resourceParameters.CaseType is not null && resourceParameters.CaseType != 0)
            {
                issueReports = issueReports.Where(i => i.CaseType.Id == resourceParameters.CaseType);
            }

            if (resourceParameters.Status is not null && resourceParameters.Status != 0)
            {
                issueReports = issueReports.Where(i => i.Status.Id == resourceParameters.Status);
            }

            if (resourceParameters.LevelOfSeverity is not null && resourceParameters.LevelOfSeverity != 0)
            {
                issueReports = issueReports.Where(i => i.Severity.Id == resourceParameters.LevelOfSeverity);
            }

            if (resourceParameters.Priority is not null && resourceParameters.Priority != 0)
            {
                issueReports = issueReports.Where(i => i.Priority.Id == resourceParameters.Priority);
            }

            if (resourceParameters.ClosureDuration is not null && resourceParameters.ClosureDuration != 0)
            {
                var DbF = EF.Functions;

                issueReports = issueReports.Where(i => i.ClosureDate != null &&
                                                       DbF.DateDiffDay(i.SubmissionDate, i.ClosureDate.Value)
                                                       ==
                                                       (int)resourceParameters.ClosureDuration);
            }

            //return await GetPaginatedData(
            //    issueReports,
            //    resourceParameters.PageNumber,
            //    resourceParameters.PageSize);
                
               return await issueReports
                .Skip(resourceParameters.PageSize * (resourceParameters.PageNumber - 1))
                .Take(resourceParameters.PageSize)
                .ToListAsync();
        }

        public async Task<bool> IssueReportExists(long IssueReportId)
        {
            return await _context.IssueReports.AnyAsync(i => i.Id == IssueReportId);
        }

        public async Task<List<IssueReport>> GetAllIssueReporstWithStatusId(long statusId)
        {
            return await _context.IssueReports.Where(i => i.StatusId == statusId && i.IsDeleted != true).ToListAsync();
        }

        public async Task<List<IssueReport>> GetAllIssueReporstWithSeveritysId(long severityId)
        {
            return await _context.IssueReports.Where(i => i.LevelSeverityId == severityId && i.IsDeleted != true).ToListAsync();
        }

        public async Task<IssueCaseStage> AddIssueCaseStage(IssueCaseStage issueCaseStage)
        {
            await _context.IssueCaseStages.AddAsync(issueCaseStage);
            return issueCaseStage;
        }

        public async Task<IssueReport> GetIssueById(long IssueReportId)
        {
            return await _context.IssueReports.Where(i => i.Id == IssueReportId && i.IsDeleted != true)
                                              .FirstOrDefaultAsync();
        }

        public void DeleteCaseStages(IList<IssueCaseStage> issueCaseStages)
        {
            _context.IssueCaseStages.RemoveRange(issueCaseStages);
        }
    }
}
