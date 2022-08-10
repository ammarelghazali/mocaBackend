using Microsoft.EntityFrameworkCore;
using MOCA.Core;
using MOCA.Core.DTOs.MeetingReservations.Request;
using MOCA.Core.DTOs.MeetingReservations.Response;
using MOCA.Core.DTOs.Shared.Responses;
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
    }
}
