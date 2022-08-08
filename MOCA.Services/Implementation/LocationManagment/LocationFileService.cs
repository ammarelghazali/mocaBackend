using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class LocationFileService : ILocationFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public LocationFileService(IAuthenticatedUserService authenticatedUserService, 
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _authenticatedUserService = authenticatedUserService ?? throw new ArgumentNullException(nameof(authenticatedUserService));
        }

        public async Task<Response<bool>> AddLocationFiles(List<LocationFileModel> request)
        {
            var locationFiles = _mapper.Map<List<LocationFile>>(request);
            for (int i = 0; i < locationFiles.Count; i++)
            {
                locationFiles[i].CreatedBy = _authenticatedUserService.UserId;
                if (locationFiles[i].CreatedAt == null || locationFiles[i].CreatedAt == default)
                {
                    locationFiles[i].CreatedAt = _dateTimeService.NowUtc;
                }
            }

            _unitOfWork.LocationFileRepo.InsertRang(locationFiles);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Add LocationFile right now");
            }

            return new Response<bool>(true, "Location File Added Successfully.");
        }

        public async Task<Response<bool>> DeleteLocationFiles(long LocationID)
        {

            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var LocationFile = await _unitOfWork.LocationFileRepoEF.DeleteAllLocationFileByLocationID(LocationID);
            if (LocationFile == false)
                return new Response<bool>("Location File With This ID didn't exist.");

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Location File right now");
            }
            return new Response<bool>(true, "Location File Deleted Successfully.");
        }

        public async Task<Response<List<LocationFileModel>>> GetLocationFilesByLocationID(long LocationID)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            if (LocationID <= 0)
            {
                return new Response<List<LocationFileModel>>("ID must be greater than zero.");
            }
            var locationFile = await _unitOfWork.LocationFileRepoEF.GetAllLocationFileByLocationID(LocationID);
            if (locationFile == null)
            {
                return new Response<List<LocationFileModel>>(null, "No LocationFile Found With This ID.");
            }
            return new Response<List<LocationFileModel>>(_mapper.Map<List<LocationFileModel>>(locationFile));
        }
    }
}
