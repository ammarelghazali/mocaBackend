using Microsoft.EntityFrameworkCore;
using MOCA.Core;
using MOCA.Core.DTOs.MeetingReservations.Request;
using MOCA.Core.DTOs.MeetingReservations.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Interfaces.MeetingSpaceReservations.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.MeetingSpaceReservations
{
    public class MeetingSpaceREservationsServices : IMeetingSpaceReservationsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public MeetingSpaceREservationsServices(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUserService)
        {
            _unitOfWork = unitOfWork;
            _authenticatedUserService = authenticatedUserService ?? throw new ArgumentNullException(nameof(authenticatedUserService));
        }

        #region CRM
        public async Task<PagedResponse<List<MeetingReservationResponseDto>>> GetAllSubmissionsWithPagination(int pageNumber, int pageSize)
        {
            pageSize = pageSize > 0 ? pageSize : 10;
            pageNumber = pageNumber > 0 ? pageNumber : 1;
            var allSubmissions =await _unitOfWork.MeetingSpaceReservationRepository.GetAllSubmissions();
            var submissions = await allSubmissions.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedResponse<List<MeetingReservationResponseDto>>(submissions, pageNumber, pageSize, submissions.Count);

        }

        public async Task<Response<List<MeetingReservationResponseDto>>> GetAllSubmissionsWithoutPagination()
        {
            var allSubmissions = await _unitOfWork.MeetingSpaceReservationRepository.GetAllSubmissions();
            var submissions = await allSubmissions.ToListAsync();
            return new Response<List<MeetingReservationResponseDto>>(submissions);
        }

        public async Task<Response<List<MeetingReservationLocationsDto>>> GetAllMeetingReservationLocations()
        {
            var locations = await _unitOfWork.MeetingSpaceReservationRepository.GetAllDistinctLocations();
            return new Response<List<MeetingReservationLocationsDto>>(locations);
        }

        public async Task<Response<MeetingReservationResponseDto>> GetMeetingReservationById(long id)
        {
            var meetingReservation = await _unitOfWork.MeetingSpaceReservationRepository.GetMeetingReservationById(id);
            return new Response<MeetingReservationResponseDto>(meetingReservation);
        }

        public async Task<PagedResponse<List<MeetingReservationResponseDto>>> GetAllMeetingReservationsWithFilter(
            GetAllMeetingReservationsWithFilterRequestDto dto)
        {
            dto.PageSize = dto.PageSize <= 0 ? 10 : dto.PageSize;
            var meetingReservation = await _unitOfWork.MeetingSpaceReservationRepository.GetAllSubmissionsWithFilter(dto);
            var allFilterdData = await meetingReservation.Take(dto.PageSize).ToListAsync();
            return new PagedResponse<List<MeetingReservationResponseDto>>(allFilterdData, 1, dto.PageSize, allFilterdData.Count);
        }
        #endregion


        #region Mobile
        
        public async Task<Response<bool>> BookMeetingReservation(BookMeetingReservationRequestDto dto)
        {
            /*
             --- public int NumOfAttendees { get; set; }
             --- public long? BasicUserId { get; set; }
             public DateTime Date { get; set; }
             public DateTime Time { get; set; }
             public long MeetingSpaceHourlyPricingId { set; get; }
             public long MeetingSpaceId { get; set; }
             public long LocationId { get; set; }
            */

            if (dto.NumOfAttendees <= 0)
                return new Response<bool>("Number of attendees must be greater than zero!");


            var x = _unitOfWork.MeetingSpaceReservationRepository.GetMeetingsWithinPeriodOfTime(dto.Date, dto.Time, dto.Time + hourse);
            if ()


            dto.BasicUserId = long.Parse(_authenticatedUserService.UserId);


        }

        #endregion
    }
}
