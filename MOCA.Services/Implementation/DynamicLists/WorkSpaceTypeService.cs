
using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs;
using MOCA.Core.DTOs.DynamicLists;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Interfaces.DynamicLists.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.DynamicLists
{
    public class WorkSpaceTypeService : IWorkSpaceTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public WorkSpaceTypeService(IAuthenticatedUserService authenticatedUserService, IMapper mapper, IUnitOfWork unitOfWork, IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _authenticatedUserService = authenticatedUserService ?? throw new ArgumentNullException(nameof(authenticatedUserService));
        }
        public Task<Response<List<WorkSpaceType>>> AddListOfWorkSpaceTypes(List<WorkSpaceTypeModel> request)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<long>> AddWorkSpaceType(WorkSpaceTypeModel request)
        {
            var workSpace = _mapper.Map<WorkSpaceType>(request);

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
            var workSpaceEntity = await _unitOfWork.WorkSpaceTypeRepoEF.IsUniqueNameAsync(request.Name);

            if (workSpaceEntity == null)
            {
                throw new InvalidOperationException("This Work Space Category is already exist");
            }

            _unitOfWork.WorkSpaceTypeRepo.Insert(workSpace);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add WorkSpaceType right now");
            }

            return new Response<long>(workSpace.Id, "WorkSpaceType Added Successfully.");
        }

        public Task<Response<bool>> DeleteWorkSpaceType(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<List<WorkSpaceTypeModel>>> GetAllWorkSpaceTypePaginated(RequestParameter filter)
        {
            throw new NotImplementedException();
        }

        public Task<Response<WorkSpaceTypeModel>> GetWorkSpaceTypeById(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<WorkSpaceTypeModel>>> GetWorkSpaceTypesWithoutPagination()
        {
            throw new NotImplementedException();
        }

        public Task<Response<long>> UpdateWorkSpaceType(WorkSpaceTypeModel request)
        {
            throw new NotImplementedException();
        }
    }
}
