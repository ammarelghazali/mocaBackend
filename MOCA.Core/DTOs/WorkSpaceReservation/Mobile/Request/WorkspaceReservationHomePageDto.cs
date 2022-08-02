using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.WorkSpaceReservation.Mobile.Request
{
    public class WorkspaceReservationHomePageDto
    {
        [Required]
        public long ClientId { get; set; }
    }
}
