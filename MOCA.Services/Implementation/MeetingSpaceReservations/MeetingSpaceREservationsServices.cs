using Microsoft.EntityFrameworkCore;
using MOCA.Core;
using MOCA.Core.DTOs.MeetingReservations.Request;
using MOCA.Core.DTOs.MeetingReservations.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MeetingSpaceReservation;
using MOCA.Core.Interfaces.MeetingSpaceReservations.Services;

namespace MOCA.Services.Implementation.MeetingSpaceReservations
{
    public class MeetingSpaceREservationsServices : IMeetingSpaceReservationsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public MeetingSpaceREservationsServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PagedResponse<List<GetAllMeetingSubmissionsResponseDto>>> GetAllSubmissionsWithPagination(int pageNumber, int pageSize)
        {
            pageSize = pageSize > 0 ? pageSize : 10;
            pageNumber = pageNumber > 0 ? pageNumber : 1;
            var allSubmissions =await _unitOfWork.MeetingSpaceReservationRepository.GetAllSubmissions();
            var submissions = await allSubmissions.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedResponse<List<GetAllMeetingSubmissionsResponseDto>>(submissions, pageNumber, pageSize, submissions.Count);

        }

        public async Task<Response<List<GetAllMeetingSubmissionsResponseDto>>> GetAllSubmissionsWithoutPagination()
        {
            var allSubmissions = await _unitOfWork.MeetingSpaceReservationRepository.GetAllSubmissions();
            var submissions = await allSubmissions.ToListAsync();
            return new Response<List<GetAllMeetingSubmissionsResponseDto>>(submissions);

        }

        public async Task<Response<List<GetAllMeetingReservationLocationsDto>>> GetAllMeetingReservationLocations()
        {
            var locations = await _unitOfWork.MeetingSpaceReservationRepository.GetAllDistinctLocations();
            return new Response<List<GetAllMeetingReservationLocationsDto>>(locations);
        }

        public async Task<Response<MeetingReservation>> GetMeetingReservationById(long id)
        {
            var meetingReservation =await _unitOfWork.MeetingSpaceReservationRepository.GetByIdAsync(id);
            return new Response<MeetingReservation>(meetingReservation);
        }


    }
}
