using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class CoworkingWorkspaceFurnishing
    {
        [Key, Column(Order = 1)]
        public long CoworkingWorkSpaceId { get; set; }
        [ForeignKey("CoworkingWorkSpaceId")]
        public virtual CoworkingWorkSpace CoworkingWorkSpace { get; set; }

        [Key, Column(Order = 2)]
        public long FurnishingId { get; set; }
        [ForeignKey("FurnishingId")]
        public virtual Furnishing Furnishing { get; set; }
    }
}
