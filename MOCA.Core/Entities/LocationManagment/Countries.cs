using MOCA.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.LocationManagment
{
    public class Countries : BaseEntity
    {
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string CountryCodeString { get; set; }
    }
}
