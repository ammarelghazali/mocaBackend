using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request
{
    public class GetWorkSpaceReservationHistoryDto
    {
        [Required]
        public long WorkSpaceReservationId { get; set; }

        [Required]
        public long ReservationTypeId { get; set; }
    }
}
