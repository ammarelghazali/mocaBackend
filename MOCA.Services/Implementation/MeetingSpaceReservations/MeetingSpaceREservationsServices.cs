using MOCA.Core;
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



    }
}
