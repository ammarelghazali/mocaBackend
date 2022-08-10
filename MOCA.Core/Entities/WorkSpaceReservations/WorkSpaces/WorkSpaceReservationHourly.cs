using MOCA.Core.Entities.WorkSpaceReservations.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces
{
    public class WorkSpaceReservationHourly : BaseWorkSpaceReservation
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public long HourId { get; set; }

        [ForeignKey("HourId")]
        public WorkSpaceHourlyPricing WorkSpaceHourlyPricing { get; set; }

        [Required]
        public decimal Price { get; set; }
        public decimal? HourlyDiscount { get; set; }

        public ICollection<WorkSpaceHourlyTopUp> TopUps { get; set; }
        public WorkSpaceHourlyTransactions WorkSpaceHourlyTransactions { get; set; }
        public WorkSpaceHourlyCancellation WorkSpaceHourlyCancellation { get; set; }
    }
}
