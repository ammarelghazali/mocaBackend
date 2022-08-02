using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Entities.Shared.Reservations;
using MOCA.Core.Entities.SSO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations.Base
{
    public class BaseWorkSpaceReservation : BaseEntity
    {
        [Required]
        public long WorkSpaceId { get; set; }
        //TODO: Add Relation To WorkSpace

        [Required]
        public long LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        [Required]
        public long BasicUserId { get; set; }

        [ForeignKey("BasicUserId")]
        public BasicUser BasicUser { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public long? PaymentMethodId { get; set; }

        [ForeignKey("PaymentMethodId")]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
