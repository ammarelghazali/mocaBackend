using MOCA.Core.Entities.Shared.Reservations;
using MOCA.Core.Interfaces.Shared.Reservations.Respositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.Shered.Reservations
{
    public class ReservationTypesRepository : GenericRepository<ReservationType>, IReservationTypesRepository
    {
        public ReservationTypesRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
