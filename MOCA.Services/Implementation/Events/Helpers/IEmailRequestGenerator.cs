using MOCA.Core.DTOs.Events.BookEventSpaceDtos.Request;
using MOCA.Core.DTOs.Shared.ThirdParty.Email;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Entities.LocationManagment;

namespace MOCA.Services.Implementation.Events.Helpers
{
    public interface IGetEmailBody
    {
        Task<EmailRequest> GetEmailRequest(Location location,
                                    EventRequester eventRequester,
                                    BooEventSpaceDto request,
                                    Industry eventIndustry,
                                    EventCategory eventCategory,
                                    List<EventSpaceTime> eventTimes,
                                    EventType eventType,
                                    List<EventSpaceVenues> venueName
                                    );

    }
}
