using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.MocaSettings.IssueReportDtos.Request;
using MOCA.Core.DTOs.MocaSettings.IssueReportDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.MocaSettings
{
    public class IssueReportService : IIssueReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IDateTimeService _dateTimeService;

        public IssueReportService(IUnitOfWork unitOfWork, IMapper mapper, 
                                  IAuthenticatedUserService authenticatedUser, IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
            _dateTimeService = dateTimeService;
        }



        public async Task<Response<InfoForNewIssueReportDto>> GetInfoForNewIssueReportAsync(Guid adminId)
        {
            var lastId = await _unitOfWork.IssueReports.GetMaxReportId();

            if (lastId == null)
                lastId = 0;

            //var reportedBy = await _unitOfWork.Users.GetAdminName(adminId);

            //if (reportedBy == "")
            //{
            //    return new Response<InfoForNewIssueReportDto>
            //    {
            //        Message = "There's no such admin"
            //    };
            //}

            return new Response<InfoForNewIssueReportDto>
            {
                Data = new InfoForNewIssueReportDto
                {
                    Id = (long)(lastId + 1),
                    SubmitionDate = DateTime.UtcNow.ToShortDateString(),
                    //ReportedBy = reportedBy
                }
            };
        }


        public async Task<Response<IssueReportDto>> GetSingleIssueReportAsync(long issueReportId)
        {
            var issueReport = await _unitOfWork.IssueReports.GetIssueReportById(issueReportId);

            if (issueReport == null)
                return new Response<IssueReportDto>
                {
                    Message = "no such an issue report found!"
                };

            var issueReportDto = _mapper.Map<IssueReportDto>(issueReport);
            //TODO: issueReportDto.ReportedBy = await _unitOfWork.Users.GetAdminName(issueReport.ReportedById);

            return new Response<IssueReportDto>(issueReportDto);
        }

        public async Task<Response<bool>> AddIssueReportAsync(IssueReportForCreationDto issueReportForCreationDto)
        {
            if (await _unitOfWork.LocationTypeRepoEF.GetByIdAsync(issueReportForCreationDto.LobSpaceTypeId) is null)
            {
                return new Response<bool>
                {
                    Message = "There's no such LOB Space Type"
                };
            }

            if (!await _unitOfWork.Priorities.PriorityExists(issueReportForCreationDto.PriorityId))
            {
                return new Response<bool>
                {
                    Message = "There's no such priority"
                };
            }

            if (!await _unitOfWork.Statuses.StatusExists(issueReportForCreationDto.StatusId))
            {
                return new Response<bool>
                {
                    Message = "There's no such status"
                };
            }

            if (!await _unitOfWork.Severities.SeverityExists(issueReportForCreationDto.LevelSeverityId))
            {
                return new Response<bool>
                {
                    Message = "There's no such severity"
                };
            }

            if (!await _unitOfWork.CaseTypes.CaseTypeExists(issueReportForCreationDto.CaseTypeId))
            {
                return new Response<bool>
                {
                    Message = "There's no such case type"
                };
            }

            var issueReport = _mapper.Map<IssueReport>(issueReportForCreationDto);
            issueReport.SubmissionDate = _dateTimeService.NowUtc;
            issueReport.OwnerId = _authenticatedUser.UserId; 

            _unitOfWork.IssueReports.Insert(issueReport);

            var issueCaseStage = new IssueCaseStage { IssueReport = issueReport };
            var addedCaseStage = await _unitOfWork.IssueReports.AddIssueCaseStage(issueCaseStage);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>
                {
                    Message = "Server Failed to Add the Issue"
                };
            }

            return new Response<bool>(true, "Issue Added Successfully");
        }



        public async Task<Response<bool>> UpdateIssueReportAsync(long issueReportId, UpdateIssueReportDto updateIssueReportDto)
        {
            var issueReport = await _unitOfWork.IssueReports.GetIssueReportById(issueReportId);

            if (issueReport is null)
            {
                return new Response<bool>
                {
                    Message = "There's no such Issue Report"
                };
            }

            if (!await _unitOfWork.Priorities.PriorityExists(updateIssueReportDto.PriorityId))
            {
                return new Response<bool>
                {
                    Message = "There's no such priority"
                };
            }

            if (!await _unitOfWork.Statuses.StatusExists(updateIssueReportDto.StatusId))
            {
                return new Response<bool>
                {
                    Message = "There's no such status"
                };
            }

            if (!await _unitOfWork.Severities.SeverityExists(updateIssueReportDto.LevelSeverityId))
            {
                return new Response<bool>
                {
                    Message = "There's no such severity"
                };
            }

            if (!await _unitOfWork.CaseTypes.CaseTypeExists(updateIssueReportDto.CaseTypeId))
            {
                return new Response<bool>
                {
                    Message = "There's no such case type"
                };
            }

            _unitOfWork.IssueReports.Delete(issueReport);

            var issueReprotToBeAdded = _mapper.Map<IssueReport>(updateIssueReportDto);

            issueReprotToBeAdded.Id = issueReportId;
            issueReprotToBeAdded.SubmissionDate = issueReport.SubmissionDate;
            issueReprotToBeAdded.ClosureDate = issueReport.ClosureDate;
            issueReprotToBeAdded.LobSpaceTypeId = issueReport.LobSpaceTypeId;
            issueReprotToBeAdded.OwnerId = _authenticatedUser.UserId;

            if (issueReprotToBeAdded.StatusId != issueReport.StatusId)
            {
                var status = await _unitOfWork.Statuses.GetByIdAsync(issueReprotToBeAdded.StatusId);

                if (status.Name == "Closed")
                {
                    issueReprotToBeAdded.ClosureDate = _dateTimeService.NowUtc;
                }

                _unitOfWork.IssueReports.Insert(issueReprotToBeAdded);

                var issueCaseStage = new IssueCaseStage { IssueReport = issueReprotToBeAdded };
                var addedCaseStage = await _unitOfWork.IssueReports.AddIssueCaseStage(issueCaseStage);
            }
            else
            {
                _unitOfWork.IssueReports.Insert(issueReprotToBeAdded);
            }

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>
                {
                    Message = "Server Failed to Update the Issue"
                };
            }

            return new Response<bool>(true, "Issue Report Updated Successfully");
        }

        public async Task<Response<bool>> DeleteIssueReportAsync(long issueReportId)
        {
            var issueReport = await _unitOfWork.IssueReports.GetIssueById(issueReportId);

            if (issueReport is null)
            {
                return new Response<bool>
                {
                    Message = "There's no such issue report"
                };
            }

            _unitOfWork.IssueReports.Delete(issueReport);

            // Delete Related Case Stages
            var caseStages = await _unitOfWork.IssueReports.GetCaseStages(issueReportId);

            _unitOfWork.IssueReports.DeleteCaseStages(caseStages);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>
                {
                    Message = "Server Failed to Delete the Issue"
                };
            }

            return new Response<bool>(true, "Issue Report Deleted Successfully");
        }


        public async Task<Response<IReadOnlyList<IssueCaseStagesDto>>> GetIssueReportCaseStages(long issueReportId)
        {
            if (!await _unitOfWork.IssueReports.IssueReportExists(issueReportId))
            {
                return new Response<IReadOnlyList<IssueCaseStagesDto>>
                {
                    Message = "There's no such issue report"
                };
            }

            var caseStages = await _unitOfWork.IssueReports.GetCaseStages(issueReportId);

            return new Response<IReadOnlyList<IssueCaseStagesDto>>(_mapper.Map<IReadOnlyList<IssueCaseStagesDto>>(caseStages));
        }

        public async Task<PagedResponse<List<IssueReportDto>>> GetPaginatedIssueReportsAsync(long? lobSpaceTypeId,
                                                               IssueReportsResourceParameters resourceParameters)
        {
            if (await _unitOfWork.LocationTypeRepoEF.GetByIdAsync(lobSpaceTypeId) is null)
            {
                return new PagedResponse<List<IssueReportDto>>(null, 0, 0, 0)
                {
                    Message = "There's no such LOB Space Type", 
                    Succeeded = false
                };
            }

            var issueReports = await _unitOfWork.IssueReports
                                                .GetReportsWithPagination(lobSpaceTypeId, resourceParameters);

            var issueReportsDto = new List<IssueReportDto>();

            foreach (var issue in issueReports)
            {
                var issueDto = _mapper.Map<IssueReportDto>(issue);
                //TODO: issueDto.ReportedBy = await _unitOfWork.Users.GetAdminName(issue.ReportedById);

                issueReportsDto.Add(issueDto);
            }

            return new PagedResponse<List<IssueReportDto>>(issueReportsDto, resourceParameters.PageNumber,
                resourceParameters.PageSize, issueReportsDto.Count);
        }
    }
}
