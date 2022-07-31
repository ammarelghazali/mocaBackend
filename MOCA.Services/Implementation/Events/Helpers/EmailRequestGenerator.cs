using System.Globalization;
using System.Text;
using Microsoft.Extensions.Options;
using MOCA.Core.DTOs.Events.BookEventSpaceDtos.Request;
using MOCA.Core.DTOs.Shared.ThirdParty.Email;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Settings;

namespace MOCA.Services.Implementation.Events.Helpers
{
    public class EmailRequestGenerator : IGetEmailBody
    {
        private readonly MailSettings _mailSettings;
        public EmailRequestGenerator(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task<EmailRequest> GetEmailRequest(Location location,
                                                        EventRequester eventRequester,
                                                        BooEventSpaceDto request,
                                                        Industry eventIndustry,
                                                        EventCategory eventCategory,
                                                        List<EventSpaceTime> eventTimes,
                                                        EventType eventType,
                                                        List<EventSpaceVenues> venueName
                                                        )
        {
            var image = "https://copolitantest1.techno-politan.xyz/templates/Artwork.png";

            StringBuilder emailStringBuilder = new StringBuilder();
            emailStringBuilder.Append("<html><TABLE><TR><TD> Location : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", location.Name);
            emailStringBuilder.Append("</strong></TD></TR><TR><TD> Event Requester : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", eventRequester.Name);
            emailStringBuilder.Append("</strong></TD></TR><TR><TD> Company Name : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", request.CompanyCommericalName);
            emailStringBuilder.Append("</strong></TD></TR>");
            if (eventIndustry != null)
            {
                emailStringBuilder.Append("<TR><TD> Industry : </TD>");
                emailStringBuilder.AppendFormat("<TD> <strong>{0}", eventIndustry.Name);
                emailStringBuilder.Append("</strong></TD></TR>");
            }
            emailStringBuilder.Append("<TR><TD> Company Website : </TD>");
            emailStringBuilder.Append("</strong></TD></TR><TR><TD> Company Website : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", request.CompanyWebsite);
            emailStringBuilder.Append("</strong></TD></TR><TR><TD> Company Facebook : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", request.CompanyFacebook);
            emailStringBuilder.Append("</strong></TD></TR><TR><TD> Company Instagram : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", request.CompanyInstgram);
            emailStringBuilder.Append("</strong></TD></TR><TR><TD> First Contact Full Name : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", request.ContactFullName1);
            emailStringBuilder.Append("</strong></TD></TR><TR><TD> Email : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", request.ContactEmail1);
            emailStringBuilder.Append("</strong></TD></TR><TR><TD> Phone : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", request.ContactMobile1);
            emailStringBuilder.Append("</strong></TD></TR><TR><TD> Second Contact Full Name : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", request.ContactFullName2);
            emailStringBuilder.Append("</strong></TD></TR><TR><TD> Email : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", request.ContactEmail2);
            emailStringBuilder.Append("</strong></TD></TR><TR><TD> Phone : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", request.ContactMobile2);
            emailStringBuilder.Append("</strong></TD></TR><TR><TD> Event Name : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", request.EventName);
            emailStringBuilder.Append("</strong></TD></TR><TR><TD> Event Category : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", eventCategory.Name);
            emailStringBuilder.Append("</strong></TD></TR><TR><TD> Other Event Category : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", request.OtherEventCategory);

            emailStringBuilder.Append("</strong></TD></TR><TR><TD> Event Description : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", request.EventDescription);
            emailStringBuilder.Append("</strong></TD></TR>");
            foreach (var item in eventTimes)
            {
                emailStringBuilder.Append("<TR><TD> Recurrence Start Date : </TD>");
                emailStringBuilder.AppendFormat("<TD> <strong>{0}", item.RecurrenceStartDate.GetValueOrDefault().ToShortTimeString());
                emailStringBuilder.Append("</strong></TD></TR>");
                if (item.RecurrenceEndDate != null)
                {

                    emailStringBuilder.Append("<TR><TD> Recurrence End Date : </TD>");
                    emailStringBuilder.AppendFormat("<TD> <strong>{0}", item.RecurrenceEndDate.Value.ToShortTimeString());
                    emailStringBuilder.Append("</strong></TD></TR>");
                }

                emailStringBuilder.Append("<TR><TD> Recurrence Start Time : </TD>");
                var startTimeTemp = Convert.ToDateTime(item.RecurrenceStartTime);
                string RecurrenceStartTime = startTimeTemp.ToString("hh:mm tt", CultureInfo.CurrentCulture);
                emailStringBuilder.AppendFormat("<TD> <strong>{0}", RecurrenceStartTime);
                emailStringBuilder.Append("</strong></TD></TR>");

                emailStringBuilder.Append("<TR><TD> Recurrence End Time : </TD>");
                var endTimeTemp = Convert.ToDateTime(item.RecurrenceEndTime);
                string RecurrenceEndTime = endTimeTemp.ToString("hh:mm tt", CultureInfo.CurrentCulture);
                emailStringBuilder.AppendFormat("<TD> <strong>{0}", RecurrenceEndTime);
                emailStringBuilder.Append("</strong></TD></TR>");

                emailStringBuilder.Append("<TR><TD> Recurrence Day : </TD>");
                emailStringBuilder.AppendFormat("<TD> <strong>{0}", item.RecurrenceDay);
                emailStringBuilder.Append("</strong></TD></TR>");
            }

            foreach (var item in venueName)
            {
                emailStringBuilder.Append("<TR><TD> Preferred Venue : </TD>");
                emailStringBuilder.AppendFormat("<TD> <strong>{0}", item.VenueName);
                emailStringBuilder.Append("</strong></TD></TR>");
            }

            emailStringBuilder.Append("<TR><TD> Need Consultancy : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", request.NeedConsultancy);
            emailStringBuilder.Append("</strong></TD></TR>");

            emailStringBuilder.Append("<TR><TD> Attendee Count : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", request.ExpectedNoAttend);
            emailStringBuilder.Append("</strong></TD></TR>");

            emailStringBuilder.Append("<TR><TD> Event Type : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", eventType.Name);
            emailStringBuilder.Append("</strong></TD></TR>");

            emailStringBuilder.Append("<TR><TD> Event Support Startup : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", request.DoesYourEventSupportStartup);
            emailStringBuilder.Append("</strong></TD></TR>");

            emailStringBuilder.Append("<TR><TD> Third Party Organizer : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", request.IsThereThirdPartyOrganizer);
            emailStringBuilder.Append("</strong></TD></TR>");

            emailStringBuilder.Append("<TR><TD> Orgnizing Company : </TD>");
            emailStringBuilder.AppendFormat("<TD> <strong>{0}", request.OrgnizingCompany);
            emailStringBuilder.Append("</strong></TD></TR>");

            emailStringBuilder.Append("</TABLE>");
            emailStringBuilder.AppendFormat("<br /> <br />Thanks{0}", Environment.NewLine);
            emailStringBuilder.Append("<br />moca spaces Team <br /></html>");
            emailStringBuilder.AppendFormat("<img src='{0}", image);
            emailStringBuilder.Append("' width='62' height='13'  alt='moca spaces' />");

            var emailRequest = new EmailRequest()
            {
                Body = emailStringBuilder.ToString(),

                To = _mailSettings.InfoCopolitanEmail,
                Subject = $"New Event Request @ {location.Name} by {request.ContactFullName1} - {request.CompanyCommericalName}",
            };

            return emailRequest;
        }

    }
}
