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
        [ForeignKey("LocationID")]
        public long LocationID { get; set; }
        public virtual Location Location { get; set; }
        [ForeignKey("LocationCurrencyID")]
        public long LocationCurrencyID { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
