using MOCA.Core.DTOs.MeetingReservations.Response;
using MOCA.Core.Entities.MeetingSpaceReservation;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.MeetingSpaceReservations.Repositories
{
    public interface IMeetingSpaceReservationRepository : IGenericRepository<MeetingReservation>
    {
        Task<IQueryable<GetAllMeetingSubmissionsResponseDto>> GetAllSubmissions();
    }
}
