using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Entities.WorkSpaceReservations.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces
{
    public class WorkSpaceReservationBundle : BaseWorkSpaceReservation
    {
        [Required]
        public long BundleId { get; set; }
        
        [ForeignKey("BundleId")]
        public WorkSpaceBundlePricing WorkSpaceBundlePricing { get; set; }

        [Required]
        public decimal BundlePrice { get; set; }

        [Required]
        public DateTime BundleStartDate { get; set; }

        [Required]
        public DateTime BundleEndDate { get; set; }

        public WorkSpaceBundleTransaction WorkSpaceBundleTransactions { get; set; }
        public WorkSpaceBundleCancellation WorkSpaceBundleCancellation { get; set; }
    }
}
