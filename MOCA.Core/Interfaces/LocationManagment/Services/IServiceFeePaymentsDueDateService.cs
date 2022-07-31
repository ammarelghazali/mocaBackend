using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared.Responses;

namespace MOCA.Core.Interfaces.LocationManagment.Services
{
    public interface IServiceFeePaymentsDueDateService
    {
        Task<Response<bool>> AddServiceFeePaymentsDueDates(List<ServiceFeePaymentsDueDateModel> request);
        Task<Response<bool>> DeleteServiceFeePaymentsDueDates(long LocationID);
        Task<Response<List<ServiceFeePaymentsDueDateModel>>> GetServiceFeePaymentsDueDatesByLocationID(long LocationID);
    }
}
