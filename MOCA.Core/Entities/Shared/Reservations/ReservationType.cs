using MOCA.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.Shared.Reservations
{
    public class ReservationType : BaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        public ICollection<ReservationTransaction> ReservationTransactions { get; set; }
        public ICollection<CancelReservation> CancelReservations { get; set; }
    }
}
