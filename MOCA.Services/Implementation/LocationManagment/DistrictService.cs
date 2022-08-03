using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.District;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Exceptions;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class DistrictService : IDistrictService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public DistrictService(
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

        public async Task<Response<long>> AddDistrict(DistrictModel request)
        {
            var district = _mapper.Map<District>(request);
            if (string.IsNullOrWhiteSpace(district.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { district.CreatedBy = _authenticatedUserService.UserId; }
            }
            if (district.CreatedAt == null || district.CreatedAt == default)
            {
                district.CreatedAt = _dateTimeService.NowUtc;
            }
            var entityCity = await _unitOfWork.CityRepo.GetByIdAsync(request.CityId);
            if (entityCity == null)
            {
                throw new NotFoundException(nameof(City), request.CityId);
            }

            _unitOfWork.DistrictRepo.Insert(district);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add District right now");
            }

            return new Response<long>(district.Id, "District Added Successfully.");
        }

        public async Task<Response<bool>> UpdateDistrict(DistrictModel request)
        {
            var district = _mapper.Map<District>(request);

            if (string.IsNullOrWhiteSpace(district.LastModifiedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("Last Modified By UserID is required");
                }
                else
                { district.LastModifiedBy = _authenticatedUserService.UserId; }
            }
            if (district.LastModifiedAt == null)
            {
                district.LastModifiedAt = DateTime.UtcNow;
            }

            var cityEntity = await _unitOfWork.CityRepo.GetByIdAsync(request.CityId);
            if (cityEntity == null) { throw new NotFoundException(nameof(City), request.CityId); }

            var districtEntity = await _unitOfWork.DistrictRepo.GetByIdAsync(request.Id);
            if (districtEntity == null) { throw new NotFoundException(nameof(District), request.Id); }
            district.CreatedBy = districtEntity.CreatedBy;
            district.CreatedAt = districtEntity.CreatedAt;

            _unitOfWork.DistrictRepo.Update(district);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Update District right now");
            }

            return new Response<bool>(true, "District Updated Successfully.");
        }

        public async Task<Response<DistrictModel>> GetDistrictByID(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            if (Id <= 0)
            {
                return new Response<DistrictModel>("ID must be greater than zero.");
            }
            var district = await _unitOfWork.DistrictRepo.GetByIdAsync(Id);
            if (district == null)
            {
                return new Response<DistrictModel>(null, "No District Found With This ID.");
            }
            return new Response<DistrictModel>(_mapper.Map<DistrictModel>(district));
        }

        public async Task<PagedResponse<List<DistrictModel>>> GetAllDistrictsWithPagination(RequestParameter filter)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            int pg_total = await _unitOfWork.DistrictRepo.GetCountAsync(x => x.IsDeleted == false);
            var data = _unitOfWork.DistrictRepo.GetPaged(filter.PageNumber,
                filter.PageSize,
                f => f.IsDeleted == false,
                q => q.OrderBy(o => o.DistrictName));

            var Res = _mapper.Map<List<DistrictModel>>(data);
            if (Res.Count == 0)
            {
                return new PagedResponse<List<DistrictModel>>(null, filter.PageNumber, filter.PageSize);
            }
            return new PagedResponse<List<DistrictModel>>(Res, filter.PageNumber, filter.PageSize, pg_total);
        }

        public async Task<Response<List<DistrictModel>>> GetAllDistrictsWithoutPagination()
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var data = _unitOfWork.DistrictRepo.GetAll();

            var Res = _mapper.Map<List<DistrictModel>>(data);
            if (Res.Count == 0)
            {
                return new Response<List<DistrictModel>>(null);
            }
            return new Response<List<DistrictModel>>(Res);
        }

        public async Task<Response<List<DistrictModel>>> GetAllDistrictsByCityID(long CityID)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            if (CityID == null || CityID <= 0) { throw new ValidationException(); }

            var data = await _unitOfWork.DistrictRepoEF.GetDistrictsByCityId(CityID);
            return new Response<List<DistrictModel>>(_mapper.Map<List<DistrictModel>>(data));
        }

        public async Task<Response<bool>> DeleteDistrict(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            var HasAnyEntities = await _unitOfWork.DistrictRepoEF.HasAnyRelatedEntities(Id);
            if (HasAnyEntities)
            {
                throw new EntityIsBusyException("District Is Busy and Can't be deleted.");
            }

            var District = await _unitOfWork.DistrictRepoEF.DeleteDistrict(Id);
            if (District == false)
                return new Response<bool>("District With This ID didn't exist.");

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete District right now");
            }
            return new Response<bool>(true, "District Deleted Successfully.");
        }
    }
}
