using AutoMapper;
using Dapper;
using MOCA.Core;
using MOCA.Core.DTOs;
using MOCA.Core.DTOs.LocationManagment.Building;
using MOCA.Core.DTOs.LocationManagment.Building.FilterParameter;
using MOCA.Core.DTOs.LocationManagment.BuildingFloor;
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

        public async Task<Response<long>> UpdateBuilding(BuildingModel request)
        {
            var building = _mapper.Map<Building>(request);

            if (string.IsNullOrWhiteSpace(building.LastModifiedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("Last Modified By UserID is required.");
                }
                else
                { building.LastModifiedBy = _authenticatedUserService.UserId; }
            }
            if (building.LastModifiedAt == null)
            {
                building.LastModifiedAt = _dateTimeService.NowUtc;
            }

            if (request.BuildingFloors.Count > 0)
            {
                var checkDeleteFloorBuilding = await _unitOfWork.BuildingFloorRepoEF.DeleteBuildingFloorByBuildingId(request.Id);
                if (checkDeleteFloorBuilding)
                {
                    var buildingFloor = _mapper.Map<List<BuildingFloor>>(request.BuildingFloors);
                    for (int i = 0; i < buildingFloor.Count; i++)
                    {
                        buildingFloor[i].LastModifiedBy = _authenticatedUserService.UserId;
                        buildingFloor[i].LastModifiedAt = _dateTimeService.NowUtc;
                    }
                    _unitOfWork.BuildingFloorRepo.InsertRang(buildingFloor);
                }
            }
            
            var buildingEntity = await _unitOfWork.BuildingRepo.GetByIdAsync(request.Id);
            building.CreatedBy = buildingEntity.CreatedBy;
            building.CreatedAt = buildingEntity.CreatedAt;

            _unitOfWork.BuildingRepo.Update(building);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Update Building right now.");
            }

            return new Response<long>(building.Id, "Building Updated Successfully.");
        }

        public async Task<Response<bool>> DeleteBuilding(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            await _unitOfWork.BuildingFloorRepoEF.DeleteBuildingFloorByBuildingId(Id);

            var building = await _unitOfWork.BuildingRepoEF.DeleteBuilding(Id);
            if (building == false)
                return new Response<bool>("Building With This ID didn't exist.");

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Building right now");
            }
            return new Response<bool>(true, "Building Deleted Successfully.");
        }

        public async Task<Response<BuildingModel>> GetBuildingByID(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            if (Id <= 0)
            {
                return new Response<BuildingModel>("ID must be greater than zero.");
            }

            var buildingFloorEntity = await _unitOfWork.BuildingFloorRepoEF.GetAllBuildingFloorByBuildingId(Id);

            var building = await _unitOfWork.BuildingRepo.GetByIdAsync(Id);
            building.BuildingFloors = buildingFloorEntity;
            if (building == null)
            {
                return new Response<BuildingModel>(null, "No Building Found With This ID.");
            }
            return new Response<BuildingModel>(_mapper.Map<BuildingModel>(building));
        }

        public async Task<Response<List<BuildingModelByLocationId>>> GetBuildingByLocationId(long LocationId)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            var location = await _unitOfWork.LocationRepo.GetByIdAsync(LocationId);
            if (location == null)
            {
                return new Response<List<BuildingModelByLocationId>>("No Location Found With This ID.");
            }

            var building = await _unitOfWork.BuildingRepoEF.GetAllBuildingByLocationId(LocationId);
            if (building == null)
            {
                return new Response<List<BuildingModelByLocationId>>(null, "No Building Found With This Location Id.");
            }
            return new Response<List<BuildingModelByLocationId>>(_mapper.Map<List<BuildingModelByLocationId>>(building));
        }

        public async Task<PagedResponse<List<GetBuildingModel>>> GetAllBuildingPaginated(RequestParameter filter)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            int pg_total = await _unitOfWork.BuildingRepo.GetCountAsync(x => x.IsDeleted == false);
            var data = _unitOfWork.BuildingRepo.GetPaged(filter.PageNumber,
                filter.PageSize,
                f => f.IsDeleted == false,
                q => q.OrderBy(o => o.Id)).ToList();

            List<GetBuildingModel> Res = new List<GetBuildingModel>();

            for (int i = 0; i < data.Count(); i++)
            {
                int buildingCount = await _unitOfWork.BuildingFloorRepoEF.CountBuildingFloorByBuildingId(data[i].Id);
                Res[i] = new GetBuildingModel
                {
                    Id = data[i].Id,
                    GrossArea = data[i].GrossArea,
                    InstallAccessPoint = data[i].InstallAccessPoint,
                    Name = data[i].Name,
                    NetArea = data[i].NetArea,
                    TotalFloors = buildingCount
                };
            }

            if (Res.Count == 0)
            {
                return new PagedResponse<List<GetBuildingModel>>(null, filter.PageNumber, filter.PageSize);
            }
            return new PagedResponse<List<GetBuildingModel>>(Res, filter.PageNumber, filter.PageSize, pg_total);
        }

        public async Task<Response<List<GetBuildingModel>>> GetAllBuildingWithoutPagination()
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            var data = _unitOfWork.BuildingRepo.GetAll().ToList();

            List<GetBuildingModel> Res = new List<GetBuildingModel>();

            for (int i = 0; i < data.Count(); i++)
            {
                int buildingCount = await _unitOfWork.BuildingFloorRepoEF.CountBuildingFloorByBuildingId(data[i].Id);
                Res[i] = new GetBuildingModel
                {
                    Id = data[i].Id,
                    GrossArea = data[i].GrossArea,
                    InstallAccessPoint = data[i].InstallAccessPoint,
                    Name = data[i].Name,
                    NetArea = data[i].NetArea,
                    TotalFloors = buildingCount
                };
            }
            if (Res.Count == 0)
            {
                return new Response<List<GetBuildingModel>>(null);
            }
            return new Response<List<GetBuildingModel>>(Res);
        }

        public async Task<PagedResponse<List<GetBuildingModel>>> GetAllFilterBuildingPaginated(GetPaginatedBuildingFilterParameter filter)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@Id", filter.Id);
            parms.Add("@@Building", filter.Building);
            parms.Add("@FromGross", filter.FromGross);
            parms.Add("@ToGross", filter.ToGross);
            parms.Add("@FromNet", filter.FromNet);
            parms.Add("@ToNet", filter.ToNet);
            parms.Add("@PageNumber", filter.PageNumber);
            parms.Add("@PageSize", filter.PageSize);
            var data = await _unitOfWork.BuildingRepo.QueryAsync<GetBuildingModel>("[dbo].[SP_Building_GetAll_Filter_Pagination]", parms, System.Data.CommandType.StoredProcedure);

            int pg_total = await _unitOfWork.BuildingRepo.GetCountAsync(x => x.IsDeleted == false);
            if (data.Count == 0)
            {
                return new PagedResponse<List<GetBuildingModel>>(null, filter.PageNumber, filter.PageSize, pg_total);
            }
            return new PagedResponse<List<GetBuildingModel>>(data.ToList(), filter.PageNumber, filter.PageSize, pg_total);
        }

        public async Task<Response<List<GetBuildingModel>>> GetAllFilterBuildingWithoutPagination(GetWithoutPaginatedBuildingFilterParameter filter)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@Id", filter.Id);
            parms.Add("@@Building", filter.Building);
            parms.Add("@FromGross", filter.FromGross);
            parms.Add("@ToGross", filter.ToGross);
            parms.Add("@FromNet", filter.FromNet);
            parms.Add("@ToNet", filter.ToNet);
            var data = await _unitOfWork.BuildingRepo.QueryAsync<GetBuildingModel>("[dbo].[SP_Building_GetAll_Filter_WithoutPagination]", parms, System.Data.CommandType.StoredProcedure);

            if (data.Count == 0)
            {
                return new Response<List<GetBuildingModel>>(null);
            }
            return new Response<List<GetBuildingModel>>(data.ToList());
        }
    }
}
