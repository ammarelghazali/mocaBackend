using MOCA.Core.Entities.EventSpaceBookings;

namespace MOCA.Core.DTOs.Events.EventOpportunityDtos.Response
{
    public class EventDetails_ViewModel
    {
        public string EventName { get; set; }
        public Model_ViewModel EventCategory { get; set; }
        public Model_ViewModel EventRecurrence { get; set; }
        public List<EventSpaceTime> eventTimes { get; set; }
        public int? ExpectedNumberOfAttendees { get; set; }
        public List<Model_ViewModel> PreferredVenue { get; set; }
        public Model_ViewModel EventType { get; set; }
        public Model_ViewModel EventAttendance { get; set; }
        public bool? EventSupportStartups { get; set; }
        public bool? PartyOrganizer { get; set; }
        public string OrganizerName { get; set; }
        public string EventDescription { get; set; }
    }
}
