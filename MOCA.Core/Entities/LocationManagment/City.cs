using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class City : BaseEntity
    {
        [ForeignKey("CountryId")]
        public long CountryId { get; set; }
        public virtual Country Country { get; set; }
        public string CityName { get; set; }
    }
}
