using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.LocationManagment
{
    public class LocationCurrency
    {
        public long LocationId { get; set; }
        public virtual Location Location { get; set; }
        public long CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
