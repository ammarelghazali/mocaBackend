using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class District : BaseEntity
    {
        public long CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        [Required]
        public string DistrictName { get; set; }
    }
}
