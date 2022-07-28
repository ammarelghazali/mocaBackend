using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class EventOpportunityStatus : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<EventSpaceBooking> EventSpaceBookings { get; set; }
    }
}
