using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class LocationContactService : ILocationContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        public LocationContactService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        public async Task<Response<bool>> AddLocationContacts(List<LocationContactModel> request)
        {
            var locationContacts = _mapper.Map<List<LocationContact>>(request);
            for (int i = 0; i < locationContacts.Count; i++)
            {
                locationContacts[i].CreatedBy = "System";
                if (locationContacts[i].CreatedAt == null || locationContacts[i].CreatedAt == default)
                {
                    locationContacts[i].CreatedAt = _dateTimeService.NowUtc;
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

            _unitOfWork.LocationContactRepo.InsertRang(locationContacts);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Add LocationContact right now");
            }*/

            return new Response<bool>(true, "LocationContact Added Successfully.");
        }

        public async Task<Response<bool>> DeleteLocationContacts(long LocationID)
        {
            /*
            if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
               {
                   throw new UnauthorizedAccessException("User is not authorized");
               }
           */

            var LocationContact = await _unitOfWork.LocationContactRepoEF.DeleteAllLocationContactByLocationID(LocationID);
            if (LocationContact == false)
                return new Response<bool>("Location Contact With This ID didn't exist.");

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Location Contact right now");
            }
            return new Response<bool>(true, "Location Contact Deleted Successfully.");
        }

        public async Task<Response<List<LocationContactModel>>> GetLocationContactsByLocationID(long LocationID)
        {
            /*
             if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
             */
            if (LocationID <= 0)
            {
                return new Response<List<LocationContactModel>>("ID must be greater than zero.");
            }
            var locationContact = await _unitOfWork.LocationContactRepoEF.GetAllLocationContactByLocationID(LocationID);
            if (locationContact == null)
            {
                return new Response<List<LocationContactModel>>(null, "No LocationContact Found With This ID.");
            }
            return new Response<List<LocationContactModel>>(_mapper.Map<List<LocationContactModel>>(locationContact));
        }
    }
}
