using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared.Exceptions;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class LocationBankAccountService : ILocationBankAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public LocationBankAccountService(
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

        public async Task<Response<long>> AddLocationBankAccount(LocationBankAccountModel request)
        {
            var locationBankAccount = _mapper.Map<LocationBankAccount>(request);
            if (string.IsNullOrWhiteSpace(locationBankAccount.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { locationBankAccount.CreatedBy = _authenticatedUserService.UserId; }
            }
            if (locationBankAccount.CreatedAt == null || locationBankAccount.CreatedAt == default)
            {
                locationBankAccount.CreatedAt = _dateTimeService.NowUtc;
            }

            var entityLocationBankAccount = await _unitOfWork.LocationBankAccountRepo.GetByIdAsync(request.Id);
            if (entityLocationBankAccount == null)
            {
                throw new NotFoundException(nameof(LocationBankAccount), request.Id);
            }

            _unitOfWork.LocationBankAccountRepo.Insert(locationBankAccount);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add Location Bank Account right now");
            }

            return new Response<long>(locationBankAccount.Id, "Location Bank Account Added Successfully.");
        }

        public async Task<Response<bool>> UpdateLocationBankAccount(LocationBankAccountModel request)
        {
            var locationBankAccount = _mapper.Map<LocationBankAccount>(request);

            if (string.IsNullOrWhiteSpace(locationBankAccount.LastModifiedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("Last Modified By UserID is required");
                }
                else
                { locationBankAccount.LastModifiedBy = _authenticatedUserService.UserId; }
            }
            if (locationBankAccount.LastModifiedAt == null)
            {
                locationBankAccount.LastModifiedAt = DateTime.UtcNow;
            }

            var locationBankAccountEntity = await _unitOfWork.LocationBankAccountRepo.GetByIdAsync(request.Id);
            if (locationBankAccountEntity == null) { throw new NotFoundException(nameof(LocationBankAccount), request.Id); }
            locationBankAccount.CreatedBy = locationBankAccountEntity.CreatedBy;
            locationBankAccount.CreatedAt = locationBankAccountEntity.CreatedAt;

            _unitOfWork.LocationBankAccountRepo.Update(locationBankAccount);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Update Location Bank Account right now");
            }

            return new Response<bool>(true, "Location Bank Account Updated Successfully.");
        }

        public async Task<Response<LocationBankAccountModel>> GetLocationBankAccountByLocationID(long ByLocationID)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            if (ByLocationID <= 0)
            {
                return new Response<LocationBankAccountModel>("ID must be greater than zero.");
            }
            var locationBankAccount = await _unitOfWork.LocationBankAccountRepoEF.GetByLocationID(ByLocationID);
            if (locationBankAccount == null)
            {
                return new Response<LocationBankAccountModel>(null, "No Location Bank Account Found With This ID.");
            }
            return new Response<LocationBankAccountModel>(_mapper.Map<LocationBankAccountModel>(locationBankAccount));
        }

        public async Task<Response<bool>> DeleteLocationBankAccountByLocationID(long ByLocationID)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            var locationBankAccount = await _unitOfWork.LocationBankAccountRepoEF.DeleteByLocationID(ByLocationID);
            if (locationBankAccount == false)
                return new Response<bool>("Location Bank Account With This ID didn't exist.");

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Location Bank Account right now");
            }
            return new Response<bool>(true, "Location Bank Account Deleted Successfully.");
        }
    }
}
