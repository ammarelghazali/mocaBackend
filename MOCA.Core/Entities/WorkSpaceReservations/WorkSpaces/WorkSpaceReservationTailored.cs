using MOCA.Core.Entities.WorkSpaceReservations.Base;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces
{
    public class WorkSpaceReservationTailored : BaseWorkSpaceReservation
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

        public ICollection<WorkSpaceTailoredTopUp> TopUps { get; set; }
        public WorkSpaceTailoredTransaction WorkSpaceTailoredTransactions { get; set; }
        public WorkSpaceTailoredCancellation WorkSpaceTailoredCancellation { get; set; }
    }
}
