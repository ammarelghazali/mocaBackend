using MOCA.Core.Entities.WorkSpaceReservations.CoWorkSpace;
using MOCA.Core.Interfaces.WorkSpaceReservations.CoworkSpace.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.WorkSpaceReservations.CoworkSpace
{
    public class CoworkSpaceTailoredTopUpRepo : GenericRepository<CoworkingSpaceTailoredTopUp>, ICoworkSpaceTailoredTopUpRepo
    {
        private readonly ApplicationDbContext _context;

        public CoworkSpaceTailoredTopUpRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
