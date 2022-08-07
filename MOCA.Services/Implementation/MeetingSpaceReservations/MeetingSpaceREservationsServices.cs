using MOCA.Core;
using MOCA.Core.DTOs.MeetingReservations.Request;
using MOCA.Core.DTOs.MeetingReservations.Response;
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
        /*
        public async Task<GetAllMeetingSubmissionsREsponseDto> GetAllSubmissionsWithPagination(GetAllMeetingsSubmissionsDto getAllMeetingsSubmissionsDto)
        {
            var allSubmissions = await _unitOfWork.MeetingSpaceReservationRepository.GetAllIQueryable();
            return allSubmissions;
        }*/
    }
}
