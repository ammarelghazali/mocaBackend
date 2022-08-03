using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class LocationIndustry : BaseEntity
    {
        public long LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
        public long? IndustryId { get; set; }
        [ForeignKey("IndustryId")]
        public virtual Industry Industry { get; set; }
    }
}
