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
        [ForeignKey("LocationID")]
        public long LocationID { get; set; }
        public virtual Location Location { get; set; }
        [ForeignKey("InclusionID")]
        public long InclusionID { get; set; }
        public virtual Inclusion Inclusion { get; set; }
    }
}
