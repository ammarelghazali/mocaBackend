using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.Currency;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Exceptions;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public CurrencyService(
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

        public async Task<Response<long>> AddCurrency(CurrencyModel request)
        {
            var currency = _mapper.Map<Currency>(request);
            if (string.IsNullOrWhiteSpace(currency.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { currency.CreatedBy = _authenticatedUserService.UserId; }
            }
            if (currency.CreatedAt == null || currency.CreatedAt == default)
            {
                currency.CreatedAt = _dateTimeService.NowUtc;
            }
            var entityCurrency = await _unitOfWork.CurrencyRepo.GetByIdAsync(request.Id);
            if (entityCurrency != null)
            {
                return new Response<long>("Currency Exists Before.");
            }

            _unitOfWork.CurrencyRepo.Insert(currency);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add Currency right now");
            }

            return new Response<long>(currency.Id, "Currency Added Successfully.");
        }

        public async Task<Response<bool>> UpdateCurrency(CurrencyModel request)
        {
            var currency = _mapper.Map<Currency>(request);

           if (string.IsNullOrWhiteSpace(currency.LastModifiedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("Last Modified By UserID is required");
                }
                else
                { currency.LastModifiedBy = _authenticatedUserService.UserId; }
            }
            if (currency.LastModifiedAt == null)
            {
                currency.LastModifiedAt = _dateTimeService.NowUtc;
            }

            var currencyEntity = await _unitOfWork.CurrencyRepo.GetByIdAsync(request.Id);
            if (currencyEntity == null) { throw new NotFoundException(nameof(Currency), request.Id); }
            currency.CreatedBy = currencyEntity.CreatedBy;
            currency.CreatedAt = currencyEntity.CreatedAt;

            _unitOfWork.CurrencyRepo.Update(currency);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Update Currency right now");
            }

            return new Response<bool>(true, "Currency Updated Successfully.");
        }

        public async Task<Response<CurrencyModel>> GetCurrencyByID(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            if (Id <= 0)
            {
                return new Response<CurrencyModel>("ID must be greater than zero.");
            }
            var currency = await _unitOfWork.CurrencyRepo.GetByIdAsync(Id);
            if (currency == null)
            {
                return new Response<CurrencyModel>(null, "No Currency Found With This ID.");
            }
            return new Response<CurrencyModel>(_mapper.Map<CurrencyModel>(currency));
        }

        public async Task<PagedResponse<List<CurrencyModel>>> GetAllCurrenciesWithPagination(RequestParameter filter)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            int pg_total = await _unitOfWork.CurrencyRepo.GetCountAsync(x => x.IsDeleted == false);

            var data = _unitOfWork.CurrencyRepo.GetPaged(filter.PageNumber,
                filter.PageSize,
                f => f.IsDeleted == false,
                q => q.OrderBy(o => o.Name));

            var Res = _mapper.Map<List<CurrencyModel>>(data);
            if (Res.Count == 0)
            {
                return new PagedResponse<List<CurrencyModel>>(null, filter.PageNumber, filter.PageSize);
            }
            return new PagedResponse<List<CurrencyModel>>(Res, filter.PageNumber, filter.PageSize, pg_total);
        }

        public async Task<Response<List<CurrencyModel>>> GetAllCurrenciesWithoutPagination()
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var data = _unitOfWork.CurrencyRepo.GetAll();

            var Res = _mapper.Map<List<CurrencyModel>>(data);
            if (Res.Count == 0)
            {
                return new Response<List<CurrencyModel>>(null, "Not Data Found");
            }
            return new Response<List<CurrencyModel>>(Res);
        }

        public async Task<Response<bool>> DeleteCurrency(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var HasAnyEntities = await _unitOfWork.CurrencyRepoEF.HasAnyRelatedEntities(Id);
            if (HasAnyEntities)
            {
                throw new EntityIsBusyException("District Is Busy and Can't be deleted.");
            }

            var currency = await _unitOfWork.CurrencyRepoEF.DeleteCurrency(Id);
            if (currency == false)
                return new Response<bool>("Currency With This ID didn't exist.");

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Currency right now");
            }
            return new Response<bool>(true, "Currency Deleted Successfully.");
        }
    }
}
