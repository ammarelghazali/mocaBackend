using MOCA.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.LocationManagment
{
    public class LocationIndustry : BaseEntity
    {
        public long LocationtID { get; set; }
        [ForeignKey("LocationtID")]
        public virtual Location Location { get; set; }
        public long? SubIndustryID { get; set; }
        [ForeignKey("SubIndustryID")]
        public virtual Industry Industry { get; set; }
    }
}
