using MOCA.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.LocationManagment
{
    public class Location : BaseEntity
    {
        public string LocationName { get; set; }
        [ForeignKey("LocationDistrictID")]
        public long LocationDistrictID { get; set; }
        public virtual District District { get; set; }
        public string LocationAddress { get; set; }
        public int LocationBuildYear { get; set; }
        public decimal LocationAreaGross { get; set; }
        public decimal LocationAreaLeasable { get; set; }
        public string LocationMapAddress { get; set; }
        public int? LocationContractLength { get; set; }
        public DateTime? LocationContractStartDate { get; set; }
        public DateTime? LocationContractEndDate { get; set; }
        public int? LocationPaymentMethods { get; set; }
        public int? LocationPartentershipType { get; set; }
    }
}
