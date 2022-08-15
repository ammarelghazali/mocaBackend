using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.DynamicLists;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.DynamicLists.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.DynamicLists
{
    public class FurnishingTypeService : IFurnishingTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public FurnishingTypeService(IAuthenticatedUserService authenticatedUserService, IMapper mapper, IUnitOfWork unitOfWork, IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _authenticatedUserService = authenticatedUserService ?? throw new ArgumentNullException(nameof(authenticatedUserService));
        }
        public async Task<Response<long>> AddFurnishingType(FurnishingTypeModel request)
        {
            var furn = _mapper.Map<FurnishingType>(request);

            if (string.IsNullOrWhiteSpace(furn.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { furn.CreatedBy = _authenticatedUserService.UserId; }
            }
            if (furn.CreatedAt == null || furn.CreatedAt == default)
            {
                furn.CreatedAt = _dateTimeService.NowUtc;
            }
            var furnEntity = await _unitOfWork.FurnishingTypeRepoEF.IsUniqueNameAsync(request.Name);

            if (!furnEntity)
            {
                return new Response<long>("This Furnishing Type is already exist");
            }

            _unitOfWork.FurnishingTypeRepo.Insert(furn);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add Furnishing Type right now");
            }

            return new Response<long>(furn.Id, "Furnishing Type Added Successfully.");
        }

        public async Task<Response<List<FurnishingType>>> AddListOfFurnishingType(List<FurnishingTypeModel> request)
        {
            var furn = _mapper.Map<List<FurnishingType>>(request);
            foreach (var item in furn)
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
                var furnEntity = await _unitOfWork.FurnishingTypeRepoEF.IsUniqueNameAsync(r.Name.ToString());
                if (!furnEntity)
                {
                    return new Response<List<FurnishingType>>("This FurnishingType is already exist");
                }
            }
            _unitOfWork.FurnishingTypeRepo.InsertRang(furn);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<List<FurnishingType>>("Cannot Add FurnishingType right now");
            }
            return new Response<List<FurnishingType>>(furn, "FurnishingType Added Successfully");
        }

        public async Task<Response<bool>> DeleteFurnishingType(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var furnEntity = await _unitOfWork.FurnishingTypeRepo.GetByIdAsync(Id);

            if (furnEntity == null)
            {
                // throw new NotFoundException(nameof(WorkSpaceCategory), Id);
                return new Response<bool>("This Furnishing Type is not exist");
            }

            await _unitOfWork.FurnishingTypeRepoEF.DeleteFurnitureType(Id);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Furnishing Type right now");
            }

            return new Response<bool>(true, "Furnishing Type Deleted Successfully.");
        }

        public async Task<PagedResponse<List<FurnishingTypeModel>>> GetAllFurnishingTypePaginated(RequestParameter filter)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            int pg_total = await _unitOfWork.FurnishingTypeRepo.GetCountAsync(x => x.IsDeleted == false);

            var data = _unitOfWork.FurnishingTypeRepo.GetPaged(filter.PageNumber,
                filter.PageSize,
                f => f.IsDeleted == false,
                q => q.OrderBy(o => o.Name));

            var Res = _mapper.Map<List<FurnishingTypeModel>>(data);
            if (Res.Count == 0)
            {
                return new PagedResponse<List<FurnishingTypeModel>>(null, filter.PageNumber, filter.PageSize);
            }
            return new PagedResponse<List<FurnishingTypeModel>>(Res, filter.PageNumber, filter.PageSize, pg_total);
        }

        public async Task<Response<FurnishingTypeModel>> GetFurnishingTypeById(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            if (Id <= 0)
            {
                return new Response<FurnishingTypeModel>("ID must be greater than zero.");
            }
            var furn = await _unitOfWork.FurnishingTypeRepo.GetByIdAsync(Id);
            if (furn == null)
            {
                return new Response<FurnishingTypeModel>("No Furnishing Type Found With This ID.");
            }
            var res = _mapper.Map<FurnishingTypeModel>(furn);
            return new Response<FurnishingTypeModel>(res);
        }

        public async Task<Response<List<FurnishingTypeModel>>> GetFurnishingTypeWithoutPagination()
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var data = _unitOfWork.FurnishingTypeRepo.GetAll().ToList();
            var Res = _mapper.Map<List<FurnishingTypeModel>>(data);

            if (Res.Count == 0)
            {
                return new Response<List<FurnishingTypeModel>>(null, "No Data Found.");
            }
            return new Response<List<FurnishingTypeModel>>(Res);
        }

        public async Task<Response<bool>> UpdateFurnishingType(FurnishingTypeModel request)
        {
            var furn = _mapper.Map<FurnishingType>(request);

            if (string.IsNullOrWhiteSpace(furn.LastModifiedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("Last Modified By UserID is required");
                }
                else
                { furn.LastModifiedBy = _authenticatedUserService.UserId; }
            }
            if (furn.LastModifiedAt == null)
            {
                furn.LastModifiedAt = DateTime.UtcNow;
            }
            var furnEntity = await _unitOfWork.FurnishingTypeRepo.GetByIdAsync(request.Id);


            if (furnEntity == null) { return new Response<bool>(false, "Cannot Update FurnishingType right now"); }

            furn.CreatedBy = furnEntity.CreatedBy;
            furn.CreatedAt = furnEntity.CreatedAt;

            _unitOfWork.FurnishingTypeRepo.Update(furn);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>(false, "Cannot Update FurnishingType right now");
            }

            return new Response<bool>(true, " FurnishingType Updated Successfully.");
        }
    }
}
