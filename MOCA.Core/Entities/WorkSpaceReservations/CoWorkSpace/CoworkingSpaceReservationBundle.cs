using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Entities.WorkSpaceReservations.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations.CoWorkSpace
{
    public class CoworkingSpaceReservationBundle : BaseCoworkSpaceReservation
    {
        [Required]  
        public long BundleId { get; set; }

        [ForeignKey("BundleId")]
        public CoworkingSpaceBundlePricing CoworkingSpaceBundlePricing { get; set; }

        [Required]
        public decimal BundlePrice { get; set; }

        [Required]
        public DateTime BundleStartDate { get; set; }

        [Required]
        public DateTime BundleEndDate { get; set; }

        public CoworkingSpaceBundleTransaction CoworkingSpaceBundleTransaction { get; set; }
        public CoworkingSpaceBundleCancellation CoworkingSpaceBundleCancellation { get; set; }
    }
}
