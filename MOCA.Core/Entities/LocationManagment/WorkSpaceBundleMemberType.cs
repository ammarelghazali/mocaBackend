using MOCA.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class WorkSpaceBundleMemberType
    {
        [Key, Column(Order = 1)]
        public long WorkSpaceBundleId { get; set; }

        [ForeignKey("WorkSpaceBundleId")]
        public WorkSpaceBundlePricing WorkSpaceBundlePricing { get; set; }

        [Key, Column(Order = 2)]
        public long MemberTypeId { get; set; }

        [ForeignKey("MemberTypeId")]
        public MemberType MemberType { get; set; }
    }
}
