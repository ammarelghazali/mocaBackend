using MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces;
using MOCA.Core.Interfaces.WorkSpaceReservations.WorkSpaces.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.WorkSpaceReservations.WorkSpaces
{
    public class WorkSpaceTailoredTopUpRepo : GenericRepository<WorkSpaceTailoredTopUp>, IWorkSpaceTailoredTopUpRepo
    {
        private readonly ApplicationDbContext _context;

        public WorkSpaceTailoredTopUpRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
