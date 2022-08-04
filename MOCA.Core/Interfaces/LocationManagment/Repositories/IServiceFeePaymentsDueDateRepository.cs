using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.LocationManagment.Repositories
{
    public interface IServiceFeePaymentsDueDateRepository : IGenericRepository<ServiceFeePaymentsDueDate>
    {
        Task<bool> DeleteAllServiceFeePaymentsDueDateByLocationID(long LocationID);
        Task<List<ServiceFeePaymentsDueDate>> GetAllServiceFeePaymentsDueDateByLocationID(long LocationID);
    }
}
