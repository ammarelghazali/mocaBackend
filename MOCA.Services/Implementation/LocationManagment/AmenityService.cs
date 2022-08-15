using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.DynamicLists;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class AmenityService : IAmenityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public AmenityService(IAuthenticatedUserService authenticatedUserService, IMapper mapper, IUnitOfWork unitOfWork, IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _authenticatedUserService = authenticatedUserService ?? throw new ArgumentNullException(nameof(authenticatedUserService));
        }
        public async Task<Response<long>> AddAmenity(AmenityModel request)
        {
            var amenity = _mapper.Map<Amenity>(request);

            if (string.IsNullOrWhiteSpace(amenity.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { amenity.CreatedBy = _authenticatedUserService.UserId; }
            }
            if (amenity.CreatedAt == null || amenity.CreatedAt == default)
            {
                amenity.CreatedAt = _dateTimeService.NowUtc;
            }
            var amenityEntity = await _unitOfWork.AmenityRepoEF.IsUniqueNameAsync(request.Name);

            if (!amenityEntity)
            {
                return new Response<long>("This Amenity is already exist");
            }

            _unitOfWork.AmenityRepo.Insert(amenity);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add Amenity right now");
            }

            return new Response<long>(amenity.Id, "Amenity Added Successfully.");
        }

        public async Task<Response<List<Amenity>>> AddListOfAmenity(List<AmenityModel> request)
        {
            var amenity = _mapper.Map<List<Amenity>>(request);
            foreach (var item in amenity)
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
                var amenityEntity = await _unitOfWork.AmenityRepoEF.IsUniqueNameAsync(r.Name.ToString());
                if (!amenityEntity)
                {
                    return new Response<List<Amenity>>("This Amenity is already exist");
                }
            }
            _unitOfWork.AmenityRepo.InsertRang(amenity);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<List<Amenity>>("Cannot Add Amenity right now");
            }
            return new Response<List<Amenity>>(amenity, "Amenity Added Successfully");
        }

        public async Task<Response<bool>> DeleteAmenity(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var amenityEntity = await _unitOfWork.AmenityRepo.GetByIdAsync(Id);

            if (amenityEntity == null)
            {
                return new Response<bool>("This Amenity is not exist");
            }

            await _unitOfWork.AmenityRepoEF.DeleteAmenity(Id);

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Amenity right now");
            }

            return new Response<bool>(true, "Amenity Deleted Successfully.");
        }

        public async Task<PagedResponse<List<AmenityModel>>> GetAllAmenityPaginated(RequestParameter filter)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            int pg_total = await _unitOfWork.AmenityRepo.GetCountAsync(x => x.IsDeleted == false);

            var data = _unitOfWork.AmenityRepo.GetPaged(filter.PageNumber,
                filter.PageSize,
                f => f.IsDeleted == false,
                q => q.OrderBy(o => o.Name));

            var Res = _mapper.Map<List<AmenityModel>>(data);
            if (Res.Count == 0)
            {
                return new PagedResponse<List<AmenityModel>>(null, filter.PageNumber, filter.PageSize);
            }
            return new PagedResponse<List<AmenityModel>>(Res, filter.PageNumber, filter.PageSize, pg_total);
        }

        public async Task<Response<AmenityModel>> GetAmenityById(long Id)
        {
            if(string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            if (Id <= 0)
            {
                return new Response<AmenityModel>("ID must be greater than zero.");
            }
            var amenity = await _unitOfWork.AmenityRepo.GetByIdAsync(Id);
            if (amenity == null)
            {
                return new Response<AmenityModel>("No amenity Found With This ID.");
            }
            var res = _mapper.Map<AmenityModel>(amenity);
            return new Response<AmenityModel>(res);
        }

        public async Task<Response<List<AmenityModel>>> GetAmenityWithoutPagination()
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var data = _unitOfWork.AmenityRepo.GetAll().ToList();
            var Res = _mapper.Map<List<AmenityModel>>(data);

            if (Res.Count == 0)
            {
                return new Response<List<AmenityModel>>(null, "No Data Found.");
            }
            return new Response<List<AmenityModel>>(Res);
        }

        public async Task<Response<bool>> UpdateAmenity(AmenityModel request)
        {
            var amenity = _mapper.Map<Amenity>(request);

            if (string.IsNullOrWhiteSpace(amenity.LastModifiedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("Last Modified By UserID is required");
                }
                else
                { amenity.LastModifiedBy = _authenticatedUserService.UserId; }
            }
            if (amenity.LastModifiedAt == null)
            {
                amenity.LastModifiedAt = DateTime.UtcNow;
            }
            var amenityEntity = await _unitOfWork.AmenityRepo.GetByIdAsync(request.Id);


            if (amenityEntity == null) { return new Response<bool>(false,"Cannot Update Amenity right now"); }

            amenity.CreatedBy = amenityEntity.CreatedBy;
            amenity.CreatedAt = amenityEntity.CreatedAt;

            _unitOfWork.AmenityRepo.Update(amenity);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>(false,"Cannot Update Amenity right now");
            }

            return new Response<bool>(true, " Amenityy Updated Successfully.");
        }

    
    }
}
