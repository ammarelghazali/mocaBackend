using MOCA.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class CoworkingSpaceBundleMemberType
    {
        [Key, Column(Order = 1)]
        public long CoworkSpaceBundleId { get; set; }

        [ForeignKey("CoworkSpaceBundleId")]
        public CoworkingSpaceBundlePricing CoworkingSpaceBundlePricing { get; set; }

        [Key, Column(Order = 2)]
        public long MemberTypeId { get; set; }

        [ForeignKey("MemberTypeId")]
        public MemberType MemberType { get; set; }
    }
}
