using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request
{
    public class CreateWorkSpaceTopUp
    {
        public string? Description { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "ReservationTypeId must be greater than zero")]
        public long ReservationTypeId { get; set; }
        public long? HourId { get; set; }
        public int? NumberOfHours { get; set; }
        public int? TailoredHours { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "WorkspaceReservationId must be greater than zero")]
        public long WorkspaceReservationId { get; set; }
    }
}
