using MOCA.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.LocationManagment
{
    public class LocationBankAccount : BaseEntity
    {
        [ForeignKey("LocationID")]
        public long LocationID { get; set; }
        public virtual Location Location { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        public string SwiftCode { get; set; }
    }
}
