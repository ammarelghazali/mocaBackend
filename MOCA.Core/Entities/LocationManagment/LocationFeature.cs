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
        public long LocationID { get; set; }
        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }
        public long FeatureID { get; set; }
        [ForeignKey("FeatureID")]
        public virtual Feature Feature { get; set; }
    }
}
