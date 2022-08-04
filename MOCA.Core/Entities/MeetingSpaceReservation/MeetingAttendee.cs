using System.ComponentModel.DataAnnotations.Schema;
using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.MeetingSpaceReservation
{
    public class MeetingAttendee : BaseEntity
    {
        public long MeetingSpaceReservationId { set; get; }
        [ForeignKey("MeetingSpaceReservationId")]
        public MeetingReservation MeetingSpaceReservation { get; set; }
        
        public MeetingSpaceReservation MeetingSpaceReservation { get; set; }
        
        public string Name { set; get; }
        public string CountryCode { set; get; }
        public string MobileNumber { set; get; }
    }
}
