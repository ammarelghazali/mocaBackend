using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        public CountryService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        public async Task<Response<long>> AddCountry(CountryModel request)
        {
            var country = _mapper.Map<Country>(request);
            if (string.IsNullOrWhiteSpace(country.CreatedBy))
            {
                /*if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { country.CreatedBy = authenticatedUser.UserId; }
                */
                country.CreatedBy = "System";
            }
            if (country.CreatedAt == null || country.CreatedAt == default)
            {
                country.CreatedAt = _dateTimeService.NowUtc;
            }

            await _repo.AddAsync(country);

            return new Response<long>(country.Id);
        }
    }
}
