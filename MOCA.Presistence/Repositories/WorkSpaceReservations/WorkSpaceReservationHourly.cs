using MOCA.Core.Interfaces.WorkSpaceReservations.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.WorkSpaceReservations
{
    public class WorkSpaceReservationHourly : GenericRepository<WorkSpaceReservationHourly>, IWorkSpaceReservationHourly
    {
        private readonly ApplicationDbContext _context;

        public WorkSpaceReservationHourly(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
