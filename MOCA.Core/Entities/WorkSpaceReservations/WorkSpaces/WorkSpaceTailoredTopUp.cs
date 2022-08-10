using MOCA.Core.Entities.WorkSpaceReservations.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces
{
    public class WorkSpaceTailoredTopUp : BaseWorkSpaceTopUp
    {
        [Required]
        public int TailoredHours { get; set; }

        [Required]
        public decimal TailoredPrice { get; set; }

        [Required]
        public long WorkSpaceReservationTailoredId { get; set; }

        [ForeignKey("WorkSpaceReservationTailoredId")]
        public WorkSpaceReservationTailored WorkSpaceReservation { get; set; }
    }
}
