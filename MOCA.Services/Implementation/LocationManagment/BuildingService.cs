using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.Building;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class BuildingService : IBuildingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public BuildingService(
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

        public async Task<Response<long>> AddBuilding(BuildingModel request)
        {
            var location = await _unitOfWork.LocationRepo.GetByIdAsync(request.LocationId);
            if (location == null)
            {
                return new Response<long>("Location Not Found.");
            }
            var buildingCheck = await _unitOfWork.BuildingRepoEF.CheckBuildingExistence(request.LocationId, request.Name);
            if (buildingCheck == false)
            {
                return new Response<long>("Building is exists before.");
            }

            var building = _mapper.Map<Building>(request);
            if (string.IsNullOrWhiteSpace(building.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { building.CreatedBy = _authenticatedUserService.UserId; }
            }
            if (building.CreatedAt == null || building.CreatedAt == default)
            {
                building.CreatedAt = _dateTimeService.NowUtc;
            }

            _unitOfWork.BuildingRepo.Insert(building);
            if (request.BuildingFloors.Count() > 0)
            {
                var buildingFloor = _mapper.Map<List<BuildingFloor>>(request.BuildingFloors);
                _unitOfWork.BuildingFloorRepo.InsertRang(buildingFloor);
            }

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add Building right now.");
            }

            return new Response<long>(building.Id, "Building Added Successfully.");
        }
    }
}
