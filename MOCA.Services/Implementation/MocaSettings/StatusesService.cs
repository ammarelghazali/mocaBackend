using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.MocaSettings.StatusDto.Request;
using MOCA.Core.DTOs.MocaSettings.StatusDto.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MOCA.Services.Implementation.MocaSettings
{
    public class StatusesService : IStatusesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StatusesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<StatusDto>>> GetAllStatusesAsync()
        {
            var statuses = await _unitOfWork.Statuses.GetAllNotDeletedAsync();
            if (statuses == null)
                return new Response<IReadOnlyList<StatusDto>>
                {
                    Message = "No statuses found!"
                };

            return new Response<IReadOnlyList<StatusDto>>(_mapper.Map<IReadOnlyList<StatusDto>>(statuses));
        }

        public async Task<Response<StatusDto>> GetSingleStatusAsync(long statusId)
        {
            var status = await _unitOfWork.Statuses.GetByIdAsync(statusId);
            if (status == null || status.IsDeleted)
                return new Response<StatusDto>
                {
                    Message = "No such a status found!"
                };

            return new Response<StatusDto>(_mapper.Map<StatusDto>(status));
        }

        public async Task<Response<StatusDto>> AddStatusyAsync(StatusForCreationDto statusForCreationDto)
        {
            if (await _unitOfWork.Statuses.StatusExists(statusForCreationDto.Name))
                return new Response<StatusDto>
                {
                    Message = "Status name is already exist"
                };

            var status = _mapper.Map<Status>(statusForCreationDto);

            _unitOfWork.Statuses.Insert(status);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<StatusDto>
                {
                    Message = "Server Error"
                };

            return new Response<StatusDto>(_mapper.Map<StatusDto>(status), "Status added successfully");
        }

        public async Task<Response<bool>> UpdateStatusAsync(long statusId, StatusForCreationDto statusForCreationDto)
        {
            var status = await _unitOfWork.Statuses.GetByIdAsync(statusId);
            if (status == null || status.IsDeleted)
                return new Response<bool>
                {
                    Message = "No such a status found!"
                };

            var isNotValidName = await _unitOfWork.Statuses.StatusExists(statusForCreationDto.Name);
            if (isNotValidName)
                return new Response<bool>
                {
                    Message = "This name is already exist"
                };


            _unitOfWork.Statuses.Delete(status);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<bool>
                {
                    Message = "Server Error!"
                };


            var statusToBeAdded = _mapper.Map<Status>(statusForCreationDto);

            _unitOfWork.Statuses.Insert(statusToBeAdded);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<bool>
                {
                    Message = "Server Error"
                };



            var issueReportsToBeUpdated = await _unitOfWork.IssueReports.GetAllIssueReporstWithStatusId(statusId);
            foreach (var issueReport in issueReportsToBeUpdated)
            {
                issueReport.StatusId = statusToBeAdded.Id;
            }

            _unitOfWork.IssueReports.UpdateRange(issueReportsToBeUpdated);

            if (await _unitOfWork.SaveAsync() < 0)
                return new Response<bool>
                {
                    Message = "Server Error"
                };


            return new Response<bool>(true, "Status updated successfully");
        }

        public async Task<Response<bool>> DeleteStatusyAsync(long statusId)
        {
            var status = await _unitOfWork.Statuses.GetByIdAsync(statusId);
            if (status == null || status.IsDeleted)
                return new Response<bool>
                {
                    Message = "No such a status found!"
                };

            _unitOfWork.Statuses.Delete(status);

            if (await _unitOfWork.SaveAsync() != 1)
                return new Response<bool>
                {
                    Message = "Server Error!"
                };

            var issueReporstToBeDeleted = await _unitOfWork.IssueReports.GetAllIssueReporstWithStatusId(statusId);
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

            return new Response<bool>(true, "Status Deleted Successfully");
        }
    }
}
