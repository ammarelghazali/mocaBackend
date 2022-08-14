using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class CoworkingWorkSpace : BaseWorkSpace
    {
        [Required]
        public long CoworkingId { get; set; }

        [ForeignKey("CoworkingId")]
        public virtual Coworking Coworking { get; set; }
        public CoworkingWorkspaceFurnishing CoworkingWorkspaceFurnishing { get; set; }
        public CoworkingWorkSpaceMarketingImage CoworkingWorkSpaceMarketingImage { get; set; }

    }
}
