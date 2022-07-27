using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class EventOpportunityStatus : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<EventSpaceBooking> EventSpace_Bookings { get; set; }
    }
}
