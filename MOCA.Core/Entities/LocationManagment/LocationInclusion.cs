using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class LocationInclusion : BaseEntity
    {
        public long LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
        public long InclusionId { get; set; }
        [ForeignKey("InclusionId")]
        public virtual Inclusion Inclusion { get; set; }
    }
}
