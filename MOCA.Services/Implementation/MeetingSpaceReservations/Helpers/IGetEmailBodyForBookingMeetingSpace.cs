using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Entities.MeetingSpaceReservation;
using MOCA.Core.Entities.SSO;

namespace MOCA.Services.Implementation.MeetingSpaceReservations.Helpers
{
    public interface IGetEmailBodyForBookingMeetingSpace
    {
        string GetEmailBody(Location location, MeetingReservation meetingReservation, 
            MeetingSpace meetingSpace, BasicUser user, MeetingSpaceHourlyPricing meetingPrice);
    }
}
