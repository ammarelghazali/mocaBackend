using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.DynamicLists;
using MOCA.Core.DTOs.Shared.Exceptions;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Interfaces.DynamicLists.Services;
using MOCA.Core.Interfaces.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Services.Implementation.DynamicLists
{
    public class WorkSpaceCategoryService : IWorkSpaceCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public WorkSpaceCategoryService(IAuthenticatedUserService authenticatedUserService, IMapper mapper, IUnitOfWork unitOfWork,IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _authenticatedUserService = authenticatedUserService ?? throw new ArgumentNullException(nameof(authenticatedUserService));
        }

        public async Task<Response<long>> AddWorkSpaceCategory(WorkSpaceCategoryModel request)
        {
          var workSpace = _mapper.Map<WorkSpaceCategory>(request);

            if (string.IsNullOrWhiteSpace(workSpace.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { workSpace.CreatedBy = _authenticatedUserService.UserId; }
            }
            if (workSpace.CreatedAt == null || workSpace.CreatedAt == default)
            {
                workSpace.CreatedAt = _dateTimeService.NowUtc;
            }
            var workSpaceEntity = _unitOfWork.WorkSpaceCategoryRepo.GetByIdAsync(request.Id);

            if (workSpaceEntity == null)
            {
                throw new NotFoundException(nameof(WorkSpaceCategory), request.Id);
            }
            _unitOfWork.WorkSpaceCategoryRepo.Insert(workSpace);

            if (await _unitOfWork.SaveAsync()< 1)
            {
                return new Response<long>("Cannot Add WorkSpaceCategory right now");
            }

            return new Response<long>(workSpace.Id, "WorkSpaceCategory Added Successfully.");

        }




        public Task<Response<List<WorkSpaceCategoryModel>>> GetAllWorkSpaceCategoryWithoutPagination()
        {
            throw new NotImplementedException();
        }

        public Task<Response<WorkSpaceCategoryModel>> GetWorkSpaceCategoryByID(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> UpdateWorkSpaceCategory(WorkSpaceCategoryModel request)
        {
            throw new NotImplementedException();
        }
    }
}
