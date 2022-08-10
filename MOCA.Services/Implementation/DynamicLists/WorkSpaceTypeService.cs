﻿
using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs;
using MOCA.Core.DTOs.DynamicLists;
using MOCA.Core.DTOs.Shared.Exceptions;
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
        public async Task<Response<List<WorkSpaceType>>> AddListOfWorkSpaceTypes(List<WorkSpaceTypeModel> request)
        {
            var workSpace = _mapper.Map<List<WorkSpaceType>>(request);

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
                var workSpaceEntity = await _unitOfWork.WorkSpaceTypeRepoEF.IsUniqueNameAsync(r.Name.ToString());
                if (!workSpaceEntity)
                {
                    return new Response<List<WorkSpaceType>>("This Work Space type is already exist");

                }
            }

            _unitOfWork.WorkSpaceTypeRepoEF.InsertRang(workSpace);



            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<List<WorkSpaceType>>("Cannot Add WorkSpaceCategory right now");
            }


            return new Response<List<WorkSpaceType>>(workSpace, "Work Space type Added Successfully");
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

            if (!workSpaceEntity)
            {
                return new Response<long>("This Work Space Category is already exist");
            }

            _unitOfWork.WorkSpaceTypeRepo.Insert(workSpace);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add WorkSpaceType right now");
            }

            return new Response<long>(workSpace.Id, "WorkSpaceType Added Successfully.");
        }

        public async Task<Response<bool>> DeleteWorkSpaceType(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var workSpaceEntity = await _unitOfWork.WorkSpaceTypeRepoEF.GetByIdAsync(Id);

            if (workSpaceEntity == null) {
               // throw new NotFoundException(nameof(WorkSpaceCategory), Id);
                return new Response<bool>("This work Space is not exist");
            }

            await _unitOfWork.WorkSpaceTypeRepoEF.DeleteWorkSpaceType(Id);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Work Space type right now");
            }

            return new Response<bool>(true, "Work Space type  Deleted Successfully.");
        }

        public Task<PagedResponse<List<WorkSpaceTypeModel>>> GetAllWorkSpaceTypePaginated(RequestParameter filter)
        {

            throw new NotImplementedException();
        }

        public async Task<Response<WorkSpaceTypeModel>> GetWorkSpaceTypeById(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            if (Id <= 0)
            {
                return new Response<WorkSpaceTypeModel>("ID must be greater than zero.");
            }
            var workSpace = await _unitOfWork.WorkSpaceTypeRepo.GetByIdAsync(Id);
            if (workSpace == null)
            {
                return new Response<WorkSpaceTypeModel>(null, "No Work Space Type Found With This ID.");
            }
            return new Response<WorkSpaceTypeModel>(_mapper.Map<WorkSpaceTypeModel>(workSpace));
        }

        public async Task<Response<List<WorkSpaceTypeModel>>> GetWorkSpaceTypesWithoutPagination()
        {
            return new Response<List<WorkSpaceTypeModel>>();
        }

        public async Task<Response<bool>> UpdateWorkSpaceType(WorkSpaceTypeModel request)
        {
            var workSpace = _mapper.Map<WorkSpaceType>(request);

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
            var workSpaceEntity = await _unitOfWork.WorkSpaceTypeRepoEF.GetByIdAsync(request.Id);


            if (workSpaceEntity == null) 
            {
               // throw new NotFoundException(nameof(WorkSpaceCategory), request.Id);
                return new Response<bool>(false, "This ID is not found");
            }

            workSpace.CreatedBy = workSpaceEntity.CreatedBy;
            workSpace.CreatedAt = workSpaceEntity.CreatedAt;

            _unitOfWork.WorkSpaceTypeRepo.Update(workSpace);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>(false,"Cannot Update Work Space type right now");
            }

            return new Response<bool>(true," Work Space type Updated Successfully.");
        }
    }
}
