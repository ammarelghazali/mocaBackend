using MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces;
using MOCA.Core.Interfaces.WorkSpaceReservations.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;


namespace MOCA.Presistence.Repositories.WorkSpaceReservations
{
    public class WorkSpaceHourlyTopUpRepo : GenericRepository<WorkSpaceHourlyTopUp>, IWorkSpaceHourlyTopUpRepo
    {
        private readonly ApplicationDbContext _context;

        public WorkSpaceHourlyTopUpRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
