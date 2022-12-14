using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.Shared.Exceptions;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Interfaces.DynamicLists.Services;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.DTOs.DynamicLists;
using MOCA.Core.Entities.DynamicLists;

namespace MOCA.Services.Implementation.DynamicLists
{
    public class WorkSpaceCategoryService : IWorkSpaceCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public WorkSpaceCategoryService(IAuthenticatedUserService authenticatedUserService, IMapper mapper, IUnitOfWork unitOfWork, IDateTimeService dateTimeService)
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
            var workSpaceEntity = await _unitOfWork.WorkSpaceCategoryRepoEF.IsUniqueNameAsync(request.Name);

            if (!workSpaceEntity)
            {
                return new Response<long>("This Work Space Category is already exist");
            }

            _unitOfWork.WorkSpaceCategoryRepo.Insert(workSpace);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add WorkSpaceCategory right now");
            }

            return new Response<long>(workSpace.Id, "WorkSpaceCategory Added Successfully.");

        }



        public async Task<Response<List<WorkSpaceCategoryModel>>> GetAllWorkSpaceCategoryWithoutPagination()
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var data = _unitOfWork.WorkSpaceCategoryRepo.GetAll().ToList();
            var Res = _mapper.Map<List<WorkSpaceCategoryModel>>(data);

            if (Res.Count == 0)
            {
                return new Response<List<WorkSpaceCategoryModel>>(null, "No Data Found.");
            }
            return new Response<List<WorkSpaceCategoryModel>>(Res);
        }



        public async Task<Response<bool>> UpdateWorkSpaceCategory(WorkSpaceCategoryModel request)
        {
            var workSpace = _mapper.Map<WorkSpaceCategory>(request);

            if (string.IsNullOrWhiteSpace(workSpace.LastModifiedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("Last Modified By UserID is required");
                }
                else
                { workSpace.LastModifiedBy = _authenticatedUserService.UserId; }
            }
            if (workSpace.LastModifiedAt == null)
            {
                workSpace.LastModifiedAt = DateTime.UtcNow;
            }
            var workSpaceEntity = await _unitOfWork.WorkSpaceCategoryRepo.GetByIdAsync(request.Id);


            if (workSpaceEntity == null) { throw new NotFoundException(nameof(WorkSpaceCategory), request.Id); }

            workSpace.CreatedBy = workSpaceEntity.CreatedBy;
            workSpace.CreatedAt = workSpaceEntity.CreatedAt;

            _unitOfWork.WorkSpaceCategoryRepo.Update(workSpace);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Update Work Space Category right now");
            }

            return new Response<bool>(true, " Work Space Category Updated Successfully.");

        }
        public async Task<Response<bool>> DeleteWorkSpaceCategory(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var DeleteWorkSpaceType = await _unitOfWork.WorkSpaceCategoryRepoEF.DeleteWorkSpaceType(Id);
            if (!DeleteWorkSpaceType)
            {
                return new Response<bool>("Work Space Category is Busy and Can't be deleted.");
            }
            var workSpaceEntity = await _unitOfWork.WorkSpaceCategoryRepo.GetByIdAsync(Id);

            if (workSpaceEntity == null) { throw new NotFoundException(nameof(WorkSpaceCategory), Id); }

            await _unitOfWork.WorkSpaceCategoryRepoEF.DeleteWorkSpaceCategory(Id);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Work Space Category right now");
            }

            return new Response<bool>(true, "Work Space Category Deleted Successfully.");

        }

        public async Task<Response<List<WorkSpaceCategory>>> AddListOfWorkSpaceCategory(List<WorkSpaceCategoryModel> request)
        {
            var workSpace = _mapper.Map<List<WorkSpaceCategory>>(request);

            foreach (var item in workSpace)
            {
                if (string.IsNullOrWhiteSpace(item.CreatedBy))
                {
                    if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                    {
                        throw new UnauthorizedAccessException("User is not authorized");
                    }
                    else
                    { item.CreatedBy = _authenticatedUserService.UserId; }
                }
                if (item.CreatedAt == null || item.CreatedAt == default)
                {
                    item.CreatedAt = _dateTimeService.NowUtc;
                }
            }

            foreach (var r in request)
            {
                var workSpaceEntity = await _unitOfWork.WorkSpaceCategoryRepoEF.IsUniqueNameAsync(r.Name.ToString());
                if (!workSpaceEntity)
                {
                    return new Response<List<WorkSpaceCategory>>("This Work Space type is already exist");

                }
            }

            _unitOfWork.WorkSpaceCategoryRepo.InsertRang(workSpace);



            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<List<WorkSpaceCategory>>("Cannot Add WorkSpaceCategory right now");
            }


            return new Response<List<WorkSpaceCategory>>(workSpace, "Work Space type Added Successfully");


        }
        

        public async Task<Response<WorkSpaceCategoryModel>> GetWorkSpaceCategoryByID(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            if (Id <= 0)
            {
                return new Response<WorkSpaceCategoryModel>("ID must be greater than zero.");
            }
            var workSpace = await _unitOfWork.WorkSpaceCategoryRepo.GetByIdAsync(Id);
            if (workSpace == null)
            {
                return new Response<WorkSpaceCategoryModel>("No City Found With This ID.");
            }
            return new Response<WorkSpaceCategoryModel>(_mapper.Map<WorkSpaceCategoryModel>(workSpace));
        }
    }
}


