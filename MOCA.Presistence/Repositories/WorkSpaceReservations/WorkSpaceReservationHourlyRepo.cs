using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.Shared.Reservations;
using MOCA.Core.Entities.WorkSpaceReservations;
using MOCA.Core.Interfaces.WorkSpaceReservations.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.WorkSpaceReservations
{
    public class WorkSpaceReservationHourlyRepo : GenericRepository<WorkSpaceReservationHourly>, IWorkSpaceReservationHourlyRepo
    {
        private readonly ApplicationDbContext _context;

        public WorkSpaceReservationHourlyRepo(ApplicationDbContext context) : base(context)
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

        public async Task<WorkSpaceReservationHourly> GetReservationInfo(long id)
        {
            return await _context.WorkSpaceReservationHourly.Where(r => r.Id == id)
                                                            .Include(r => r.Location)
                                                            .ThenInclude(r => r.LocationType)
                                                            .Include(r => r.TopUps)
                                                            .ThenInclude(r => r.PaymentMethod)
                                                            .Include(r => r.BasicUser).FirstOrDefaultAsync();
        }
    }
}
