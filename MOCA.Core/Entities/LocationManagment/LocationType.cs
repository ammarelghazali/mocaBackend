using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.EventSpaceBookings;

namespace MOCA.Core.Entities.LocationManagment
{
    public class LocationType : BaseEntity
    {
        public string Name { get; set; }
        public IList<EventSpaceBooking> EventSpaceBookings { get; set; }
    }
}
