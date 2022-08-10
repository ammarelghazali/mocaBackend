using MOCA.Core.Entities.WorkSpaceReservations.Base;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.WorkSpaceReservations.CoWorkSpace
{
    public class CoworkingSpaceReservationTailored : BaseCoworkSpaceReservation
    {
        [Required]
        public DateTime TailoredStartDate { get; set; }

        [Required]
        public DateTime TailoredEndDate { get; set; }

        [Required]
        public int TailoredHours { get; set; }

        [Required]
        public decimal TailoredPrice { get; set; }

        public decimal? TailoredDiscount { get; set; }

        public ICollection<CoworkingSpaceTailoredTopUp> TopUps { get; set; }
        public CoworkingSpaceTailoredTransaction CoworkingSpaceTailoredTransaction { get; set; }
        public CoworkingSpaceTailoredCancellation CoworkingSpaceTailoredCancellation { get; set; }
    }
}
