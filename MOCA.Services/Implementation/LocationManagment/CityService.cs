using AutoMapper;
using Compolitan.Core.DTOs;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.City;
using MOCA.Core.DTOs.Shared.Exceptions;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        public CityService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        public async Task<Response<long>> AddCity(CityModel request)
        {
            var city = _mapper.Map<City>(request);
            city.CreatedBy = "System";
            /*if (string.IsNullOrWhiteSpace(city.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { city.CreatedBy = authenticatedUserService.UserId; }
            }*/
            if (city.CreatedAt == null || city.CreatedAt == default)
            {
                city.CreatedAt = _dateTimeService.NowUtc;
            }
            var entityCountry = await _unitOfWork.CountryRepo.GetByIdAsync(request.CountryId);
            if (entityCountry == null) 
            { 
                throw new NotFoundException(nameof(Country), request.CountryId);
            }

            _unitOfWork.CityRepo.Insert(city);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add City right now");
            }*/

            return new Response<long>(city.Id, "City Added Successfully.");
        }

        public async Task<Response<bool>> UpdateCity(CityModel request)
        {
            var city = _mapper.Map<City>(request);

            /*if (string.IsNullOrWhiteSpace(country.LastModifiedBy))
            {
                if (string.IsNullOrWhiteSpace(authenticatedUser.UserId))
                {
                    throw new UnauthorizedAccessException("Last Modified By UserID is required");
                }
                else
                { country.LastModifiedBy = authenticatedUser.UserId; }
            }*/
            city.LastModifiedBy = "System";
            if (city.LastModifiedAt == null)
            {
                city.LastModifiedAt = DateTime.UtcNow;
            }

            var countryEntity = await _unitOfWork.CountryRepo.GetByIdAsync(request.CountryId);
            if (countryEntity == null) { throw new NotFoundException(nameof(Country), request.CountryId); }

            var cityEntity = await _unitOfWork.CityRepo.GetByIdAsync(request.Id);
            if (cityEntity == null) { throw new NotFoundException(nameof(City), request.Id); }
            city.CreatedBy = cityEntity.CreatedBy;
            city.CreatedAt = cityEntity.CreatedAt;

            _unitOfWork.CityRepo.Update(city);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Update Country right now");
            }*/

            return new Response<bool>(true, "City Updated Successfully.");
        }

        public async Task<Response<CityModel>> GetCityByID(long Id)
        {
            /*
             if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
             */
            if (Id <= 0)
            {
                return new Response<CityModel>("ID must be greater than zero.");
            }
            var city = await _unitOfWork.CityRepo.GetByIdAsync(Id);
            if (city == null)
            {
                return new Response<CityModel>(null, "No City Found With This ID.");
            }
            return new Response<CityModel>(_mapper.Map<CityModel>(city));
        }

        public async Task<PagedResponse<List<CityModel>>> GetAllCityWithPagination(RequestParameter filter)
        {
            /*
             if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
            */
            int pg_total = await _unitOfWork.CityRepo.GetCountAsync(x => x.IsDeleted == false);
            var data = _unitOfWork.CityRepo.GetPaged(filter.PageNumber, 
                filter.PageSize,
                f => f.IsDeleted == false,
                q => q.OrderBy(o => o.CityName));

            var Res = _mapper.Map<List<CityModel>>(data);
            if (Res.Count == 0)
            {
                return new PagedResponse<List<CityModel>>(null, filter.PageNumber, filter.PageSize);
            }
            return new PagedResponse<List<CityModel>>(Res, filter.PageNumber, filter.PageSize, pg_total);
        }

        public async Task<Response<List<CityModel>>> GetAllCityWithoutPagination()
        {
            /*
             if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
            */
            var data = _unitOfWork.CityRepo.GetAll();

            var Res = _mapper.Map<List<CityModel>>(data);
            if (Res.Count == 0)
            {
                return new Response<List<CityModel>>(null);
            }
            return new Response<List<CityModel>>(Res);
        }

        public async Task<Response<List<CityModel>>> GetAllCityByCountryID(long CountryID)
        {
            /*
             if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
            */
            
            if (CountryID == null || CountryID <= 0) { throw new ValidationException(); }

            var data = await _unitOfWork.CityRepoEF.GetCitiesByCountryId(CountryID);
            return new Response<List<CityModel>>(_mapper.Map<List<CityModel>>(data));
        }

        public async Task<Response<bool>> DeleteCity(long Id)
        {
            /*
            if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
               {
                   throw new UnauthorizedAccessException("User is not authorized");
               }
           */
            var HasAnyDistricts = await _unitOfWork.CityRepoEF.HasAnyDistricts(Id);
            if (HasAnyDistricts)
            {
                throw new EntityIsBusyException("City Is Busy and Can't be deleted.");
            }

            var city = await _unitOfWork.CityRepoEF.DeleteCity(Id);
            if (city == false)
                return new Response<bool>("City With This ID didn't exist.");

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete City right now");
            }
            return new Response<bool>(true, "City Deleted Successfully.");
        }
    }
}
