using Microsoft.EntityFrameworkCore;
using MOCA.Core;
using MOCA.Core.DTOs.MeetingReservations.Request;
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

       


       


        public async Task<Response<MeetingReservation>> GetMeetingReservationById(long id)
        {
            var meetingReservation =await _unitOfWork.MeetingSpaceReservationRepository.GetByIdAsync(id);
            return new Response<MeetingReservation>(meetingReservation);
        }

        public async Task<Response<List<string>>> GetAllMeetingReservationLocations()
        {
            var meetingReservations = await _unitOfWork.MeetingSpaceReservationRepository.GetAllIQueryable();
            var allLocations = await meetingReservations.Include(x => x.Location.Name)
                                    .Select(x => x.Location.Name).Distinct().ToListAsync();

            return new Response<List<string>>(allLocations);
        }


        public async Task GetAllFilteredMeetingREservations()
        {

        }
    }
}
