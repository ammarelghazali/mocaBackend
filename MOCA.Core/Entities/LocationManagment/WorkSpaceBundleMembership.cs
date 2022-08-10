using MOCA.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class WorkSpaceBundleMembership
    {
        [Required]
        public long WorkSpaceBundleId { get; set; }

        [ForeignKey("WorkSpaceBundleId")]
        public WorkSpaceBundlePricing WorkSpaceBundlePricing { get; set; }

        [Required]
        public long MemberTypeId { get; set; }

        [ForeignKey("MemberTypeId")]
        public MemberType MemberType { get; set; }
    }
}
