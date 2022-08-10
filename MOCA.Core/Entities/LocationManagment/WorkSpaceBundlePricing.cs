using MOCA.Core.Entities.LocationManagment.Base;

namespace MOCA.Core.Entities.LocationManagment
{
    public class WorkSpaceBundlePricing : BaseWorkSpaceBundlePricing
    {
        [Required]
        public long WorkSpaceId { get; set; }

        [ForeignKey("WorkSpaceId")]
        public WorkSpace WorkSpace { get; set; }
    }
}
