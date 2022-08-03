using MOCA.Core.Entities.Shared.Reservations;
using MOCA.Core.Entities.WorkSpaceReservations;
using MOCA.Core.Interfaces.Base;
namespace MOCA.Core.Interfaces.WorkSpaceReservations.Repositories
{
    public interface IWorkSpaceReservationTailoredRepo : IGenericRepository<WorkSpaceReservationTailored>
    {
        Task<WorkSpaceReservationTailored> GetReservationInfo(long id);
        Task<ReservationTransaction> GetRelatedReservationTransaction(long Reservationid, long reservationTypeId);
    }
}
