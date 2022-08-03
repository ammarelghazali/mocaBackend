using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class LocationInclusionService : ILocationInclusionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public LocationInclusionService(
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

        public async Task<Response<bool>> AddLocationInclusions(List<LocationInclusionModel> request)
        {
            var locationInclusions = _mapper.Map<List<LocationInclusion>>(request);
            for (int i = 0; i < locationInclusions.Count; i++)
            {
                locationInclusions[i].CreatedBy = _authenticatedUserService.UserId;
                if (locationInclusions[i].CreatedAt == null || locationInclusions[i].CreatedAt == default)
                {
                    locationInclusions[i].CreatedAt = _dateTimeService.NowUtc;
                }
            }

            _unitOfWork.LocationInclusionRepo.InsertRang(locationInclusions);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Add LocationInclusion right now");
            }

            return new Response<bool>(true, "LocationInclusion Added Successfully.");
        }

        public async Task<Response<bool>> DeleteLocationInclusions(long LocationID)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            var LocationInclusion = await _unitOfWork.LocationInclusionRepoEF.DeleteAllLocationInclusionByLocationID(LocationID);
            if (LocationInclusion == false)
                return new Response<bool>("Location Inclusion With This ID didn't exist.");

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Location Inclusion right now");
            }
            return new Response<bool>(true, "Location Inclusion Deleted Successfully.");
        }

        public async Task<Response<List<LocationInclusionModel>>> GetLocationInclusionsByLocationID(long LocationID)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            if (LocationID <= 0)
            {
                return new Response<List<LocationInclusionModel>>("ID must be greater than zero.");
            }
            var locationInclusion = await _unitOfWork.LocationInclusionRepoEF.GetAllLocationInclusionByLocationID(LocationID);
            if (locationInclusion == null)
            {
                return new Response<List<LocationInclusionModel>>(null, "No Location Inclusion Found With This ID.");
            }
            return new Response<List<LocationInclusionModel>>(_mapper.Map<List<LocationInclusionModel>>(locationInclusion));
        }
    }
}
