using MOCA.Core;
using MOCA.Core.Interfaces.MeetingSpaceReservations.Services;

namespace MOCA.Services.Implementation.MeetingSpaceReservations
{
    public class MeetingReservationTopUpsServices : IMeetingReservationTopUpsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public MeetingReservationTopUpsServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
