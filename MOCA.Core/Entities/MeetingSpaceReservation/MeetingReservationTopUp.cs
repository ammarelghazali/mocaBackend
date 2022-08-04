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
        

        public long? PaymentMethodId { get; set; }
        [ForeignKey("PaymentMethodId")]
        public PaymentMethod PaymentMethod { get; set; }
        

        public long MeetingRoomTimePriceId { set; get; }
        //[ForeignKey("MeetingRoomTimePriceId")]
        //public MeetingRoomTimePrice MeetingRoomTimePrice { get; set; }
        
        public string Description { set; get; }

    }
}
