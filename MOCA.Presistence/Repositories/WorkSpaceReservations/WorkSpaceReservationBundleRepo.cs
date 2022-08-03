using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.Shared.Reservations;
using MOCA.Core.Entities.WorkSpaceReservations;
using MOCA.Core.Interfaces.WorkSpaceReservations.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.WorkSpaceReservations
{
    public class WorkSpaceReservationBundleRepo : GenericRepository<WorkSpaceReservationBundle>, IWorkSpaceReservationBundleRepo
    {
        private readonly ApplicationDbContext _context;

        public WorkSpaceReservationBundleRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ReservationTransaction> GetRelatedReservationTransaction(long Reservationid, long reservationTypeId)
        {
            return await _context.ReservationTransactions.Where(r => r.ReservationTypeId == reservationTypeId &&
                                                                                r.ReservationTargetId == Reservationid)
                                                                    .Include(r => r.ReservationDetails)
                                                                    .Include(r => r.ReservationType)
                                                                    .FirstOrDefaultAsync();
        }

        public async Task<WorkSpaceReservationBundle> GetReservationInfo(long id)
        {
            return await _context.WorkSpaceReservationBundle.Where(r => r.Id == id)
                                                            .Include(r => r.Location)
                                                            .ThenInclude(r => r.LocationType)
                                                            .Include(r => r.BasicUser).FirstOrDefaultAsync();
        }
    }
}
