using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class LocationCurrencyService : ILocationCurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        public LocationCurrencyService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        public async Task<Response<bool>> AddLocationCurrencies(List<LocationCurrencyModel> request)
        {
            var locationCurrencies = _mapper.Map<List<LocationCurrency>>(request);
            for (int i = 0; i < locationCurrencies.Count; i++)
            {
                locationCurrencies[i].CreatedBy = "System";
                if (locationCurrencies[i].CreatedAt == null || locationCurrencies[i].CreatedAt == default)
                {
                    locationCurrencies[i].CreatedAt = _dateTimeService.NowUtc;
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

            _unitOfWork.LocationCurrencyRepo.InsertRang(locationCurrencies);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Add Location Currency right now");
            }*/

            return new Response<bool>(true, "Location Currency Added Successfully.");
        }

        public async Task<Response<bool>> DeleteLocationCurrencies(long LocationID)
        {
            /*
            if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
               {
                   throw new UnauthorizedAccessException("User is not authorized");
               }
           */

            var LocationCurrency = await _unitOfWork.LocationCurrencyRepoEF.DeleteByLocationID(LocationID);
            if (LocationCurrency == false)
                return new Response<bool>("Location Currency With This ID didn't exist.");

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Location Currency right now");
            }
            return new Response<bool>(true, "Location Currency Deleted Successfully.");
        }

        public async Task<Response<List<LocationCurrencyModel>>> GetLocationCurrenciesByLocationID(long LocationID)
        {
            /*
             if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
             */
            if (LocationID <= 0)
            {
                return new Response<List<LocationCurrencyModel>>("ID must be greater than zero.");
            }
            var locationCurrency = await _unitOfWork.LocationCurrencyRepoEF.GetByLocationID(LocationID);
            if (locationCurrency == null)
            {
                return new Response<List<LocationCurrencyModel>>(null, "No LocationContact Found With This ID.");
            }
            return new Response<List<LocationCurrencyModel>>(_mapper.Map<List<LocationCurrencyModel>>(locationCurrency));
        }
    }
}
