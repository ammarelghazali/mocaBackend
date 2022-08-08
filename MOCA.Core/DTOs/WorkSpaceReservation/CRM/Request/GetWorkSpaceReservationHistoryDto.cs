using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request
{
    public class GetWorkSpaceReservationHistoryDto
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Workspace Reservation Id must be greater than zero")]
        public long WorkSpaceReservationId { get; set; }

        [Required]
        public long ReservationTypeId { get; set; }
    }
}
