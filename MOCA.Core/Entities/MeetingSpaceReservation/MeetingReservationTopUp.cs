using System.ComponentModel.DataAnnotations.Schema;
using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.Shared.Reservations;

namespace MOCA.Core.Entities.MeetingSpaceReservation
{
    public class MeetingReservationTopUp : BaseEntity
    {
        public long MeetingSpaceReservationId { set; get; }

        [ForeignKey("MeetingSpaceReservationId")]
        public MeetingSpaceReservation meetingSpaceReservation { set; get; }
        
        [ForeignKey("PaymentMethodId")]
        public PaymentMethod PaymentMethod { get; set; }
        
        public string Description { set; get; }
        public int TotalHours { set; get; }
        public decimal? TotalPrice { set; get; }
        public long? PaymentMethodId { get; set; }

    }
}
