using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class ServiceFeePaymentsDueDateService : IServiceFeePaymentsDueDateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public ServiceFeePaymentsDueDateService(
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

        public async Task<Response<bool>> AddServiceFeePaymentsDueDates(List<ServiceFeePaymentsDueDateModel> request)
        {
            var serviceFeePaymentsDueDates = _mapper.Map<List<ServiceFeePaymentsDueDate>>(request);
            for (int i = 0; i < serviceFeePaymentsDueDates.Count; i++)
            {
                serviceFeePaymentsDueDates[i].CreatedBy = _authenticatedUserService.UserId;
                if (serviceFeePaymentsDueDates[i].CreatedAt == null || serviceFeePaymentsDueDates[i].CreatedAt == default)
                {
                    serviceFeePaymentsDueDates[i].CreatedAt = _dateTimeService.NowUtc;
                }
            }

            _unitOfWork.ServiceFeePaymentsDueDateRepo.InsertRang(serviceFeePaymentsDueDates);
            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Add Service Fee Payments Due Dates right now");
            }

            return new Response<bool>(true, "Service Fee Payments Due Dates Added Successfully.");
        }

        public async Task<Response<bool>> DeleteServiceFeePaymentsDueDates(long LocationID)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            var ServiceFeePaymentsDueDate = await _unitOfWork.ServiceFeePaymentsDueDateRepoEF.DeleteAllServiceFeePaymentsDueDateByLocationID(LocationID);
            if (ServiceFeePaymentsDueDate == false)
                return new Response<bool>("Service Fee Payments Due Date With This ID didn't exist.");

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Service Fee Payments Due Date right now");
            }
            return new Response<bool>(true, "Service Fee Payments Due Date Deleted Successfully.");
        }

        public async Task<Response<List<ServiceFeePaymentsDueDateModel>>> GetServiceFeePaymentsDueDatesByLocationID(long LocationID)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            if (LocationID <= 0)
            {
                return new Response<List<ServiceFeePaymentsDueDateModel>>("ID must be greater than zero.");
            }
            var locationContact = await _unitOfWork.ServiceFeePaymentsDueDateRepoEF.GetAllServiceFeePaymentsDueDateByLocationID(LocationID);
            if (locationContact == null)
            {
                return new Response<List<ServiceFeePaymentsDueDateModel>>(null, "No Service Fee Payments Due Date Found With This ID.");
            }
            return new Response<List<ServiceFeePaymentsDueDateModel>>(_mapper.Map<List<ServiceFeePaymentsDueDateModel>>(locationContact));
        }
    }
}
