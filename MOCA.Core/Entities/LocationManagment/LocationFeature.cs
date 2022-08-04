using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class LocationFeature : BaseEntity
    {
        public long LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
        public long FeatureId { get; set; }
        [ForeignKey("FeatureId")]
        public virtual Feature Feature { get; set; }
    }
}
