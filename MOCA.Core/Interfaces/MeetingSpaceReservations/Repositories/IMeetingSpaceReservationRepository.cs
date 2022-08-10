using MOCA.Core.DTOs.MeetingReservations.Request;
using MOCA.Core.DTOs.MeetingReservations.Response;
using MOCA.Core.Entities.MeetingSpaceReservation;
using MOCA.Core.Interfaces.Base;

namespace MOCA.Core.Interfaces.MeetingSpaceReservations.Repositories
{
    public interface IMeetingSpaceReservationRepository : IGenericRepository<MeetingReservation>
    {
        Task<IQueryable<MeetingReservationResponseDto>> GetAllSubmissions();
        Task<List<MeetingReservationLocationsDto>> GetAllDistinctLocations();
        Task<MeetingReservationResponseDto> GetMeetingReservationById(long id);
        Task<IQueryable<MeetingReservationResponseDto>> GetAllSubmissionsWithFilter(
            GetAllMeetingReservationsWithFilterRequestDto dto);

    }
}
