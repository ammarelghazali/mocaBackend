using MOCA.Core.DTOs.MeetingReservations.Request;
using MOCA.Core.DTOs.MeetingReservations.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MeetingSpaceReservation;

namespace MOCA.Core.Interfaces.MeetingSpaceReservations.Services
{
    public interface IMeetingSpaceReservationsServices
    {
        #region CRM
        Task<PagedResponse<List<MeetingReservationResponseDto>>> GetAllSubmissionsWithPagination(int pageNumber, int pageSize);
        Task<Response<List<MeetingReservationResponseDto>>> GetAllSubmissionsWithoutPagination();
        Task<PagedResponse<List<MeetingReservationResponseDto>>> GetAllMeetingReservationsWithFilter(
            GetAllMeetingReservationsWithFilterRequestDto dto);
        Task<Response<MeetingReservationResponseDto>> GetMeetingReservationById(long id);
        Task<Response<List<MeetingReservationLocationsDto>>> GetAllMeetingReservationLocations();
        #endregion

        #region Mobile
        Task<Response<bool>> BookMeetingReservation(BookMeetingReservationRequestDto dto);
        Task<Response<bool>> AddAttendees(List<MeetingAttendeeDto> dto);
        Task<Response<bool>> UpdatePaymentMethod(long meetingReservationId, long paymentMethodId);
        Task<Response<List<OccupiedTimesDto>>> GetAllOccupiedTimeInDay(DateTime Day, long meetingSpaceId);


        #endregion

    }
}
