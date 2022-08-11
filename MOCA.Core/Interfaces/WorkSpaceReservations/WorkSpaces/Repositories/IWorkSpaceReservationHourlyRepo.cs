using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Entities.Shared.Reservations;
using MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.WorkSpaceReservations.WorkSpaces.Repositories
{
    public interface IWorkSpaceReservationHourlyRepo : IGenericRepository<WorkSpaceReservationHourly>
    {
        Task<WorkSpaceReservationHourly> GetReservationInfo(long id);
        Task<WorkSpaceReservationHourly> GetReservationById(long id);
        Task<IQueryable<GetAllWorkSpaceReservationsResponse>> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request);
    }
}
