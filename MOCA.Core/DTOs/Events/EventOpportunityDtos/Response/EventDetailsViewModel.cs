using MOCA.Core.Entities.EventSpaceBookings;

namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Response
{
    public class EventDetailsViewModel
    {
        public string EventName { get; set; }
        public ModelViewModel EventCategory { get; set; }
        public ModelViewModel EventRecurrence { get; set; }
        public List<EventSpaceTime> eventTimes { get; set; }
        public int? ExpectedNumberOfAttendees { get; set; }
        public List<ModelViewModel> PreferredVenue { get; set; }
        public ModelViewModel EventType { get; set; }
        public ModelViewModel EventAttendance { get; set; }
        public bool? EventSupportStartups { get; set; }
        public bool? PartyOrganizer { get; set; }
        public string OrganizerName { get; set; }
        public string EventDescription { get; set; }
    }
}
