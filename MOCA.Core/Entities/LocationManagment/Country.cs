using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.LocationManagment
{
    public class Country : BaseEntity
    {
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string CountryCodeString { get; set; }
    }
}
