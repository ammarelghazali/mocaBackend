using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.Feature;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Exceptions;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class FeatureService : IFeatureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public FeatureService(
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

        public async Task<Response<long>> AddFeature(FeatureModel request)
        {
            var feature = _mapper.Map<Feature>(request);
            if (string.IsNullOrWhiteSpace(feature.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { feature.CreatedBy = _authenticatedUserService.UserId; }
            }
            if (feature.CreatedAt == null || feature.CreatedAt == default)
            {
                feature.CreatedAt = _dateTimeService.NowUtc;
            }
            var entityFeature = await _unitOfWork.FeatureRepo.GetByIdAsync(request.Id);
            if (entityFeature != null)
            {
                return new Response<long>("Currency Exists Before.");
            }

            _unitOfWork.FeatureRepo.Insert(feature);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add Feature right now");
            }

            return new Response<long>(feature.Id, "Feature Added Successfully.");
        }

        public async Task<Response<bool>> UpdateFeature(FeatureModel request)
        {
            var feature = _mapper.Map<Feature>(request);

            if (string.IsNullOrWhiteSpace(feature.LastModifiedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("Last Modified By UserID is required");
                }
                else
                { feature.LastModifiedBy = _authenticatedUserService.UserId; }
            }
            if (feature.LastModifiedAt == null)
            {
                feature.LastModifiedAt = DateTime.UtcNow;
            }

            var featureEntity = await _unitOfWork.FeatureRepo.GetByIdAsync(request.Id);
            if (featureEntity == null) { throw new NotFoundException(nameof(Feature), request.Id); }
            feature.CreatedBy = featureEntity.CreatedBy;
            feature.CreatedAt = featureEntity.CreatedAt;

            _unitOfWork.FeatureRepo.Update(feature);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Update Feature right now");
            }

            return new Response<bool>(true, "Feature Updated Successfully.");
        }

        public async Task<Response<FeatureModel>> GetFeatureByID(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            if (Id <= 0)
            {
                return new Response<FeatureModel>("ID must be greater than zero.");
            }
            var feature = await _unitOfWork.FeatureRepo.GetByIdAsync(Id);
            if (feature == null)
            {
                return new Response<FeatureModel>(null, "No Feature Found With This ID.");
            }
            return new Response<FeatureModel>(_mapper.Map<FeatureModel>(feature));
        }

        public async Task<PagedResponse<List<FeatureModel>>> GetAllFeaturesWithPagination(RequestParameter filter)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            int pg_total = await _unitOfWork.FeatureRepo.GetCountAsync(x => x.IsDeleted == false);
            var data = _unitOfWork.FeatureRepo.GetPaged(filter.PageNumber,
                filter.PageSize,
                f => f.IsDeleted == false,
                q => q.OrderBy(o => o.Name));

            var Res = _mapper.Map<List<FeatureModel>>(data);
            if (Res.Count == 0)
            {
                return new PagedResponse<List<FeatureModel>>(null, filter.PageNumber, filter.PageSize);
            }
            return new PagedResponse<List<FeatureModel>>(Res, filter.PageNumber, filter.PageSize, pg_total);
        }

        public async Task<Response<List<FeatureModel>>> GetAllFeaturesWithoutPagination()
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var data = _unitOfWork.FeatureRepo.GetAll();

            var Res = _mapper.Map<List<FeatureModel>>(data);
            if (Res.Count == 0)
            {
                return new Response<List<FeatureModel>>(null, "Not Data Found");
            }
            return new Response<List<FeatureModel>>(Res);
        }

        public async Task<Response<bool>> DeleteFeature(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            var HasAnyRelatedEntities = await _unitOfWork.FeatureRepoEF.HasAnyRelatedEntities(Id);
            if (HasAnyRelatedEntities)
            {
                throw new EntityIsBusyException("Feature Is Busy and Can't be deleted.");
            }

            var feature = await _unitOfWork.FeatureRepoEF.DeleteFeature(Id);
            if (feature == false)
                return new Response<bool>("Feature With This ID didn't exist.");

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Feature right now");
            }
            return new Response<bool>(true, "Feature Deleted Successfully.");
        }
    }
}
