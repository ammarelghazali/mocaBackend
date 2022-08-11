using MOCA.Core.Entities.LocationManagment.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
