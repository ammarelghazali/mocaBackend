using MOCA.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.LocationManagment
{
    public class LocationFeature : BaseEntity
    {
        public long LocationId { get; set; }
        public virtual Location Location { get; set; }
        public long FeatureId { get; set; }
        public virtual Feature Feature { get; set; }
    }
}
