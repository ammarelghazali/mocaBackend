using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class EventAttendance : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<EventSpaceBooking> EventSpaceBookings { get; set; }
    }
}
