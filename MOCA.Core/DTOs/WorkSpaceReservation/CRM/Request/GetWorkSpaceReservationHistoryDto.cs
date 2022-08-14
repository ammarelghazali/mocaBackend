using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request
{
    public class GetWorkSpaceReservationHistoryDto
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Workspace Reservation Id must be greater than zero")]
        public long WorkSpaceReservationId { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "ReservationTypeId must be greater than zero")]
        public long ReservationTypeId { get; set; }
    }
}
