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
        [ForeignKey("LocationID")]
        public long LocationID { get; set; }
        public virtual Location Location { get; set; }
        [ForeignKey("FeatureID")]
        public long FeatureID { get; set; }
        public virtual Feature Feature { get; set; }
    }
}
