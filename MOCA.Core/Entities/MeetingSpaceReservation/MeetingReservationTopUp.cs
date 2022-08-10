using System.ComponentModel.DataAnnotations.Schema;
using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Entities.Shared.Reservations;

namespace MOCA.Core.Entities.MeetingSpaceReservation
{
    public class MeetingReservationTopUp : BaseEntity
    {
        public string Description { set; get; }

        public long MeetingReservationId { set; get; }
        [ForeignKey("MeetingReservationId")]
        public MeetingReservation MeetingReservation { set; get; }
        

        public long? PaymentMethodId { get; set; }
        [ForeignKey("PaymentMethodId")]
        public PaymentMethod PaymentMethod { get; set; }

        public long MeetingSpaceHourlyPricingId { set; get; }
        [ForeignKey("MeetingSpaceHourlyPricingId")]
        public MeetingSpaceHourlyPricing MeetingSpaceHourlyPricing { get; set; }
    }
}
