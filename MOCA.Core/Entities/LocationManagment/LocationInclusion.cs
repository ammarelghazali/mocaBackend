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
        public long LocationId { get; set; }
        public virtual Location Location { get; set; }
        public long InclusionId { get; set; }
        public virtual Inclusion Inclusion { get; set; }
    }
}
