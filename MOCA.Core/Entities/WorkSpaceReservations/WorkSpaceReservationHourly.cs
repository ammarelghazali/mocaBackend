using MOCA.Core.Entities.WorkSpaceReservations.Base;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.WorkSpaceReservations
{
    public class WorkSpaceReservationHourly : BaseWorkSpaceReservation
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public long HourId { get; set; }
        //TODO: Add Relation With LoungeLocationPricing

        [Required]
        public decimal Price { get; set; }  
        public decimal? HourlyDiscount { get; set; }

        public ICollection<WorkSpaceHourlyTopUp> WorkSpaceHourlyTopUps { get; set; }
    }
}
