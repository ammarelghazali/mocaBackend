using MOCA.Core.Entities.MeetingSpaceReservation;
using MOCA.Core.Interfaces.MeetingSpaceReservations.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.MeetingSpaceReservations
{
    public class MeetingReservationTransactionRepository : GenericRepository<MeetingReservationTransaction>,
                                                            IMeetingReservationTransactionRepository
    {
        public MeetingReservationTransactionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
