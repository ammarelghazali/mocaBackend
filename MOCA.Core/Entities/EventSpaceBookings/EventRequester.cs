﻿using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class EventRequester : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<EventSpaceBooking> EventSpace_Bookings { get; set; }
    }
}
