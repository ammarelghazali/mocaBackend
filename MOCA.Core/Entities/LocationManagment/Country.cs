using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.LocationManagment
{
    public class Country : BaseEntity
    {
        [Required]
        public string CountryName { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string CountryCodeString { get; set; }
    }
}
