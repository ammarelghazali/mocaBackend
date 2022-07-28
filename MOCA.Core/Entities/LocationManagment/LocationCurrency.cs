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
        public long LocationID { get; set; }
        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }
        public long LocationCurrencyID { get; set; }
        [ForeignKey("LocationCurrencyID")]
        public virtual Currency Currency { get; set; }
    }
}
