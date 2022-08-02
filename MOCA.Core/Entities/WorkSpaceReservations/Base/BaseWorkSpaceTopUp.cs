using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.Shared.Reservations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations.Base
{
    public class BaseWorkSpaceTopUp : BaseEntity
    {
        [MaxLength(1000)]
        public string? Description { get; set; }
        public long? PaymentMethodId { get; set; }

        [ForeignKey("PaymentMethodId")]
        public PaymentMethod MyProperty { get; set; }
    }
}
