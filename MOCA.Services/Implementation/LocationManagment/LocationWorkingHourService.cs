using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class LocationWorkingHourService : ILocationWorkingHourService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        public LocationWorkingHourService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        public async Task<Response<bool>> AddLocationWorkingHours(List<LocationWorkingHourModel> request)
        {
            var locationWorkingHours = _mapper.Map<List<LocationWorkingHour>>(request);
            for (int i = 0; i < locationWorkingHours.Count; i++)
            {
                locationWorkingHours[i].CreatedBy = "System";
                if (locationWorkingHours[i].CreatedAt == null || locationWorkingHours[i].CreatedAt == default)
                {
                    locationWorkingHours[i].CreatedAt = _dateTimeService.NowUtc;
                }
            }
            /*if (string.IsNullOrWhiteSpace(city.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { city.CreatedBy = authenticatedUserService.UserId; }
            }*/

            _unitOfWork.LocationWorkingHourRepo.InsertRang(locationWorkingHours);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Add Location Working Hours right now");
            }*/

            return new Response<bool>(true, "Location Working Hours Added Successfully.");
        }

        public async Task<Response<bool>> DeleteLocationWorkingHours(long LocationID)
        {
            /*
            if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
               {
                   throw new UnauthorizedAccessException("User is not authorized");
               }
           */

            var locationWorkingHours = await _unitOfWork.LocationWorkingHourRepoEF.DeleteAllLocationWorkingHourByLocationID(LocationID);
            if (locationWorkingHours == false)
                return new Response<bool>("Location Working Hours With This ID didn't exist.");

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Location Working Hours right now");
            }
            return new Response<bool>(true, "Location Working Hours Deleted Successfully.");
        }

        public async Task<Response<List<LocationWorkingHourModel>>> GetLocationWorkingHoursByLocationID(long LocationID)
        {
            /*
             if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
             */
            if (LocationID <= 0)
            {
                return new Response<List<LocationWorkingHourModel>>("ID must be greater than zero.");
            }
            var locationContact = await _unitOfWork.LocationWorkingHourRepoEF.GetAllLocationWorkingHourByLocationID(LocationID);
            if (locationContact == null)
            {
                return new Response<List<LocationWorkingHourModel>>(null, "No Location Working Hour Found With This ID.");
            }
            return new Response<List<LocationWorkingHourModel>>(_mapper.Map<List<LocationWorkingHourModel>>(locationContact));
        }
    }
}
