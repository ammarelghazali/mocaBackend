using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.LocationType;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Exceptions;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class LocationTypeService : ILocationTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public LocationTypeService(
            IAuthenticatedUserService authenticatedUserService, 
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _authenticatedUserService = authenticatedUserService ?? throw new ArgumentNullException(nameof(authenticatedUserService));
        }

        public async Task<Response<long>> AddLocationType(LocationTypeModel request)
        {
            var locationType = _mapper.Map<LocationType>(request);
            if (string.IsNullOrWhiteSpace(locationType.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { locationType.CreatedBy = _authenticatedUserService.UserId; }
            }
            if (locationType.CreatedAt == null || locationType.CreatedAt == default)
            {
                locationType.CreatedAt = _dateTimeService.NowUtc;
            }

            var entitylocationType = await _unitOfWork.LocationTypeRepo.GetByIdAsync(request.Id);
            if (entitylocationType == null)
            {
                throw new NotFoundException(nameof(LocationType), request.Id);
            }

            _unitOfWork.LocationTypeRepo.Insert(locationType);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add LocationType right now");
            }

            return new Response<long>(locationType.Id, "LocationType Added Successfully.");
        }

        public async Task<Response<bool>> UpdateLocationType(LocationTypeModel request)
        {
            var locationType = _mapper.Map<LocationType>(request);

            if (string.IsNullOrWhiteSpace(locationType.LastModifiedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("Last Modified By UserID is required");
                }
                else
                { locationType.LastModifiedBy = _authenticatedUserService.UserId; }
            }
            if (locationType.LastModifiedAt == null)
            {
                locationType.LastModifiedAt = DateTime.UtcNow;
            }

            var locationTypeEntity = await _unitOfWork.LocationTypeRepo.GetByIdAsync(request.Id);
            if (locationTypeEntity == null) { throw new NotFoundException(nameof(LocationType), request.Id); }
            locationType.CreatedBy = locationTypeEntity.CreatedBy;
            locationType.CreatedAt = locationTypeEntity.CreatedAt;

            _unitOfWork.LocationTypeRepo.Update(locationType);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Update locationType right now");
            }

            return new Response<bool>(true, "LocationType Updated Successfully.");
        }

        public async Task<Response<LocationTypeModel>> GetLocationTypeByID(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            if (Id <= 0)
            {
                return new Response<LocationTypeModel>("ID must be greater than zero.");
            }
            var locationType = await _unitOfWork.LocationTypeRepo.GetByIdAsync(Id);
            if (locationType == null)
            {
                return new Response<LocationTypeModel>(null, "No LocationType Found With This ID.");
            }
            return new Response<LocationTypeModel>(_mapper.Map<LocationTypeModel>(locationType));
        }

        public async Task<PagedResponse<List<LocationTypeModel>>> GetAllLocationTypesWithPagination(RequestParameter filter)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            int pg_total = await _unitOfWork.LocationTypeRepo.GetCountAsync(x => x.IsDeleted == false);
            var data = _unitOfWork.LocationTypeRepo.GetPaged(filter.PageNumber,
                filter.PageSize,
                f => f.IsDeleted == false,
                q => q.OrderBy(o => o.Name));

            var Res = _mapper.Map<List<LocationTypeModel>>(data);
            if (Res.Count == 0)
            {
                return new PagedResponse<List<LocationTypeModel>>(null, filter.PageNumber, filter.PageSize);
            }
            return new PagedResponse<List<LocationTypeModel>>(Res, filter.PageNumber, filter.PageSize, pg_total);
        }

        public async Task<Response<List<LocationTypeModel>>> GetAllLocationTypesWithoutPagination()
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var data = _unitOfWork.LocationTypeRepo.GetAll();

            var Res = _mapper.Map<List<LocationTypeModel>>(data);
            if (Res.Count == 0)
            {
                return new Response<List<LocationTypeModel>>(null);
            }
            return new Response<List<LocationTypeModel>>(Res);
        }

        public async Task<Response<bool>> DeleteLocationType(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            var locationType = await _unitOfWork.LocationTypeRepoEF.DeleteLocationType(Id);
            if (locationType == false)
                return new Response<bool>("LocationType With This ID didn't exist.");

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete LocationType right now");
            }
            return new Response<bool>(true, "LocationType Deleted Successfully.");
        }
    }
}
