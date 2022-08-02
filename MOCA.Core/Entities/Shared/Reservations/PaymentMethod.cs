using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.Shared.Reservations
{
    public class PaymentMethod : BaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
    }
}
