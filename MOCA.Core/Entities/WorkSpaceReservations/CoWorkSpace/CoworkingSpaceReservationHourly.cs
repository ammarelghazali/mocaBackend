using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Entities.WorkSpaceReservations.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations.CoWorkSpace
{
    public class CoworkingSpaceReservationHourly : BaseCoworkSpaceReservation
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public long HourId { get; set; }

        [ForeignKey("HourId")]
        public CoWorkingSpaceHourlyPricing CoWorkingSpaceHourlyPricing { get; set; }

        [Required]
        public bool IsDay { get; set; }

        [Required]
        public decimal Price { get; set; }
        public decimal? HourlyDiscount { get; set; }

        public ICollection<CoworkingSpaceHourlyTop> TopUps { get; set; }
        public CoworkingSpaceHourlyTransaction CoworkingSpaceHourlyTransaction { get; set; }
        public CoworkingSpaceHourlyCancellation CoworkingSpaceHourlyCancellation { get; set; }
    }
}
