using MOCA.Core.Entities.WorkSpaceReservations.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations.CoWorkSpace
{
    public class CoworkingSpaceTailoredTopUp : BaseWorkSpaceTopUp
    {
        [Required]
        public int TailoredHours { get; set; }

        [Required]
        public decimal TailoredPrice { get; set; }

        [Required]
        public long CoworkingSpaceReservationTailoredId { get; set; }

        [ForeignKey("CoworkingSpaceReservationTailoredId")]
        public CoworkingSpaceReservationTailored CoworkingSpaceReservation { get; set; }
    }
}
