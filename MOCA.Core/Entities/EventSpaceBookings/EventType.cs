using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class EventType : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<EventSpaceBooking> EventSpace_Bookings { get; set; }
        public bool IsDeleted { get ;set; }
    }
}
