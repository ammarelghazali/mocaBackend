using MOCA.Core.Entities.WorkSpaceReservations.Base;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces
{
    public class WorkSpaceReservationBundle : BaseWorkSpaceReservation
    {
        [Required]
        public long PackageId { get; set; }
        //TODO: Add Relations With Packages

        [Required]
        public decimal PackagePrice { get; set; }

        [Required]
        public DateTime PackageStartDate { get; set; }

        [Required]
        public DateTime PackageEndDate { get; set; }

        public decimal? PackageDiscount { get; set; }

        public WorkSpaceBundleTransactions WorkSpaceBundleTransactions { get; set; }
        public WorkSpaceBundleCancellation WorkSpaceBundleCancellation { get; set; }
    }
}
