using Microsoft.Extensions.Options;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Entities.MeetingSpaceReservation;
using MOCA.Core.Entities.SSO;

namespace MOCA.Services.Implementation.MeetingSpaceReservations.Helpers
{
    public class GetEmailBodyForBookingMeetingSpace : IGetEmailBodyForBookingMeetingSpace
    {
        public string GetEmailBody(Location location, MeetingReservation meetingReservation, 
            MeetingSpace meetingSpace, BasicUser user, MeetingSpaceHourlyPricing meetingPrice)
        {
            var image = "https://copolitantest1.techno-politan.xyz/templates/Artwork.png";
            var body = $"<html> " +
                        $"<br /> Client Name : <strong>{user.FirstName + " " + user.LastName} </strong>" +
                        $"<br /> Mobile Number : <strong>{user.MobileNumber}</strong>" +
                        $"<br /> Location : <strong>{location.Name}</strong>" +
                        $"<br /> Meeting Room : <strong>{meetingSpace.VenueName}</strong>" +
                        $"<br /> Booking Date : <strong>{(meetingReservation.DateAndTime).ToLongDateString()}</strong>" +
                        $"<br /> Booking Time : <strong>{(meetingReservation.DateAndTime).ToShortTimeString()}</strong>" +
                        $"<br /> Number Of Hours : <strong>{meetingPrice.Hours}</strong>" +
                        $"<br /> Number Of Attendees : <strong>{meetingReservation.NumOfAttendees}</strong>" +
                        $"<br /> Total Price : <strong>{Math.Round(meetingPrice.TotalPrice, 2)}</strong>" +
                        $"<br /> " +
                        $"<br /> Platform : <strong>'Mobile'</strong>" +
                        $"<br /> " +
                        $"<br /> <br />Thanks" + Environment.NewLine + "<br />moca spaces Team <br /></html>" +
                        $" <img src='{image}' width='62' height='13'  alt='moca spaces' />";
            return body;

        }
    }
}
