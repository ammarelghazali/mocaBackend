using MOCA.Core.DTOs.MeetingReservations.Request;
using MOCA.Core.DTOs.MeetingReservations.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MeetingSpaceReservation;

namespace MOCA.Core.Interfaces.MeetingSpaceReservations.Services
{
    public interface IMeetingSpaceReservationsServices
    {
        Task<PagedResponse<List<GetAllMeetingSubmissionsResponseDto>>> GetAllSubmissionsWithPagination();
        Task<Response<List<GetAllMeetingSubmissionsResponseDto>>> GetAllSubmissionsWithoutPagination();
    }
}
