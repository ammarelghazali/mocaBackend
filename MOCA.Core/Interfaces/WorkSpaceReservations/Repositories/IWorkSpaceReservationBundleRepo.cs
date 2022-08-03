using MOCA.Core.Entities.Shared.Reservations;
using MOCA.Core.Entities.WorkSpaceReservations;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.Repositories
{
    public interface IWorkSpaceReservationBundleRepo : IGenericRepository<WorkSpaceReservationBundle>
    {
        Task<WorkSpaceReservationBundle> GetReservationInfo(long id);
        Task<ReservationTransaction> GetRelatedReservationTransaction(long Reservationid, long reservationTypeId);
    }
}
