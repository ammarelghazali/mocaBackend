using MOCA.Core;
using MOCA.Core.Interfaces.MeetingSpaceReservations.Services;

namespace MOCA.Services.Implementation.MeetingSpaceReservations
{
    public class MeetingAttendeesServices : IMeetingAttendeesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public MeetingAttendeesServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
