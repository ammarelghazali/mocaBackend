using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Entities.SSO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.Shared.Reservations
{
    public class BaseReservationEntity :  BaseEntity
    {
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
        public PaymentMethod? PaymentMethod { get; set; }
    }
}