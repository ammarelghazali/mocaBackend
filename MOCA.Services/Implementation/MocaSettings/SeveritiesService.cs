using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.MocaSettings.SeverityDtos.Request;
using MOCA.Core.DTOs.MocaSettings.SeverityDtos.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MOCA.Services.Implementation.MocaSettings
{
    public class SeveritiesService : ISeveritiesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SeveritiesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task<Response<IReadOnlyList<SeverityDto>>> GetAllSeverityAsync()
        {
            var severities = await _unitOfWork.Severities.GetAllNotDeletedAsync();
            if (severities == null)
                return new Response<IReadOnlyList<SeverityDto>>
                {
                    Message = "No Severities found!"
                };

            return new Response<IReadOnlyList<SeverityDto>>(_mapper.Map<IReadOnlyList<SeverityDto>>(severities));
        }

        public async Task<Response<SeverityDto>> GetSingleSeverityAsync(long severityId)
        {
            var severity = await _unitOfWork.Severities.GetByIdAsync(severityId);
            if (severity == null || severity.IsDeleted)
                return new Response<SeverityDto>
                {
                    Message = "No such a Severity found!"
                };

            return new Response<SeverityDto>(_mapper.Map<SeverityDto>(severity));
        }

        public async Task<Response<SeverityDto>> AddSeverityAsync(SeverityForCreationDto severityForCreationDto)
        {
            if (await _unitOfWork.Severities.SeverityExists(severityForCreationDto.Name))
                return new Response<SeverityDto>
                {
                    Message = "Severity name is already exist"
                };

            var severity = _mapper.Map<Severity>(severityForCreationDto);
            _unitOfWork.Severities.Insert(severity);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<SeverityDto>
                {
                    Message = "Server Error"
                };

            return new Response<SeverityDto>(_mapper.Map<SeverityDto>(severity), "Severity added successfully");
        }

        public async Task<Response<bool>> UpdateSeverityAsync(long severityId, SeverityForCreationDto severityForCreationDto)
        {
            var severity = await _unitOfWork.Severities.GetByIdAsync(severityId);
            if (severity == null || severity.IsDeleted)
                return new Response<bool>
                {
                    Message = "No such a Severity found!"
                };

            var isNotValidName = await _unitOfWork.Severities.SeverityExists(severityForCreationDto.Name);
            if (isNotValidName)
                return new Response<bool>
                {
                    Message = "This name is already exist"
                };


            _unitOfWork.Severities.Delete(severity);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<bool>
                {
                    Message = "Server Error!"
                };


            var severityToBeAdded = _mapper.Map<Severity>(severityForCreationDto);

            _unitOfWork.Severities.Insert(severityToBeAdded);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<bool>
                {
                    Message = "Server Error"
                };



            var issueReportsToBeUpdated = await _unitOfWork.IssueReports.GetAllIssueReporstWithSeveritysId(severityId);
            foreach (var issueReport in issueReportsToBeUpdated)
            {
                issueReport.LevelSeverityId = severityToBeAdded.Id;
            }

            _unitOfWork.IssueReports.UpdateRange(issueReportsToBeUpdated);

            if (await _unitOfWork.SaveAsync() < 0)
                return new Response<bool>
                {
                    Message = "Server Error"
                };


            return new Response<bool>(true, "Severity updated successfully");
        }

        public async Task<Response<bool>> DeleteSeverityAsync(long severityId)
        {
            var severity = await _unitOfWork.Severities.GetByIdAsync(severityId);
            if (severity == null || severity.IsDeleted)
                return new Response<bool>
                {
                    Message = "No such a Severity found!"
                };

            _unitOfWork.Severities.Delete(severity);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<bool>
                {
                    Message = "Server Error!"
                };

            var issueReporstToBeDeleted = await _unitOfWork.IssueReports.GetAllIssueReporstWithSeveritysId(severityId);
            foreach (var issueReport in issueReporstToBeDeleted)
            {
                issueReport.IsDeleted = true;
            }

            _unitOfWork.IssueReports.UpdateRange(issueReporstToBeDeleted);

            if (await _unitOfWork.SaveAsync() < 0)
                return new Response<bool>
                {
                    Message = "Server Error"
                };

            return new Response<bool>(true, "Severity Deleted Successfully");
        }
    }
}
