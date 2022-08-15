using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class CoworkingWorkSpaceMarketingImage
    {
        [Key, Column(Order = 1)]
        public long CoworkingWorkSpaceId { get; set; }
        [ForeignKey("CoworkingWorkSpaceId")]
        public virtual CoworkingWorkSpace CoworkingWorkSpace { get; set; }

        [Key, Column(Order = 2)]
        public long MarketingImagesId { get; set; }
        [ForeignKey("MarketingImagesId")]
        public virtual MarketingImages MarketingImages { get; set; }
    }
}
