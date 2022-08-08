using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.LocationManagment
{
    public class ServiceFeePaymentsDueDateRepository : GenericRepository<ServiceFeePaymentsDueDate>, IServiceFeePaymentsDueDateRepository
    {
        private readonly ApplicationDbContext _context;
        public ServiceFeePaymentsDueDateRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAllServiceFeePaymentsDueDateByLocationID(long LocationID)
        {
            var ServiceFeePaymentsDueDates = _context.ServiceFeePaymentsDueDates.Where(x => x.LocationId == LocationID && x.IsDeleted != true).ToList();

            _context.ServiceFeePaymentsDueDates.RemoveRange(ServiceFeePaymentsDueDates);
            return true;
        }

        public async Task<List<ServiceFeePaymentsDueDate>> GetAllServiceFeePaymentsDueDateByLocationID(long LocationID)
        {
            var ServiceFeePaymentsDueDates = _context.ServiceFeePaymentsDueDates.Where(x => x.LocationId == LocationID && x.IsDeleted != true).ToList();
            return new List<ServiceFeePaymentsDueDate>(ServiceFeePaymentsDueDates);
        }
    }
}
