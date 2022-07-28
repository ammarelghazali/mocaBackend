using MOCA.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.LocationManagment
{
    public class LocationInclusion : BaseEntity
    {
        public long LocationID { get; set; }
        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }
        public long InclusionID { get; set; }
        [ForeignKey("InclusionID")]
        public virtual Inclusion Inclusion { get; set; }
    }
}
