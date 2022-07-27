using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class City
    {
        [ForeignKey("CountryId")]
        public long CountryId { get; set; }
        public virtual Country Country { get; set; }
        public string CityName { get; set; }
    }
}
