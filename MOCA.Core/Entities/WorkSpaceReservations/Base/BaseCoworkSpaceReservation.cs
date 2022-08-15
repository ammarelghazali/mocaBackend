using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Entities.Shared.Reservations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations.Base
{
    public class BaseCoworkSpaceReservation : BaseReservationEntity
    {
        [Required]
        public long CoworkingId { get; set; }

        [ForeignKey("CoworkingId")]
        public Coworking Coworking { get; set; }
    }
}
