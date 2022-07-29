﻿using AutoMapper;
using Compolitan.Core.DTOs;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.Country;
using MOCA.Core.DTOs.Shared.Exceptions;
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
            country.CreatedBy = "System";
            /*if (string.IsNullOrWhiteSpace(country.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { country.CreatedBy = authenticatedUser.UserId; }
            }*/
            if (country.CreatedAt == null || country.CreatedAt == default)
            {
                country.CreatedAt = _dateTimeService.NowUtc;
            }

            _unitOfWork.CountryRepo.Insert(country);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add Country right now");
            }*/

            return new Response<long>(country.Id, "Country Added Successfully.");
        }

        public async Task<Response<bool>> UpdateCountry(CountryModel request)
        {
            var country = _mapper.Map<Country>(request);

            /*if (string.IsNullOrWhiteSpace(country.LastModifiedBy))
            {
                if (string.IsNullOrWhiteSpace(authenticatedUser.UserId))
                {
                    throw new UnauthorizedAccessException("Last Modified By UserID is required");
                }
                else
                { country.LastModifiedBy = authenticatedUser.UserId; }
            }*/
            country.LastModifiedBy = "System";
            if (country.LastModifiedAt == null) 
            {
                country.LastModifiedAt = DateTime.UtcNow;
            }

            var countryEntity = await _unitOfWork.CountryRepo.GetByIdAsync(request.Id);

            if (countryEntity == null) { throw new NotFoundException(nameof(Country), request.Id); }

            country.CreatedBy = countryEntity.CreatedBy;
            country.CreatedAt = countryEntity.CreatedAt;

            _unitOfWork.CountryRepo.Update(country);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Update Country right now");
            }*/

            return new Response<bool>(true, "Country Updated Successfully.");
        }

        public async Task<Response<CountryModel>> GetCountryByID(long Id)
        {
            /*
             if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
             */
            if (Id <= 0) 
            { 
                return new Response<CountryModel>("ID must be greater than zero."); 
            }
            var country = await _unitOfWork.CountryRepo.GetByIdAsync(Id);
            if (country == null)
            {
                return new Response<CountryModel>(null, "No Country Found With This ID.");
            }
            return new Response<CountryModel>(_mapper.Map<CountryModel>(country));
        }

        public async Task<PagedResponse<List<CountryModel>>> GetAllCountryWithPagination(RequestParameter filter)
        {
            /*
             if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
            */
            int pg_total = await _unitOfWork.CountryRepo.GetCountAsync(x => x.IsDeleted == false);
            var data = _unitOfWork.CountryRepo.GetPaged(filter.PageNumber, filter.PageSize, f => f.IsDeleted == false, q => q.OrderBy(o => o.CountryName));

            var Res = _mapper.Map<List<CountryModel>>(data);
            if (Res.Count == 0)
            {
                return new PagedResponse<List<CountryModel>>(null, filter.PageNumber, filter.PageSize);
            }
            return new PagedResponse<List<CountryModel>>(Res, filter.PageNumber, filter.PageSize, pg_total);
        }

        public async Task<Response<List<CountryModel>>> GetAllCountryWithoutPagination()
        {
            /*
             if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
            */
            var data = _unitOfWork.CountryRepo.GetAll();

            var Res = _mapper.Map<List<CountryModel>>(data);
            if (Res.Count == 0)
            {
                return new Response<List<CountryModel>>(null);
            }
            return new Response<List<CountryModel>>(Res);
        }
    }
}
