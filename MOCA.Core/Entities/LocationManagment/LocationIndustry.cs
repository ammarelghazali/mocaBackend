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
        [ForeignKey("LocationtID")]
        public long LocationtID { get; set; }
        public virtual Location Location { get; set; }
        [ForeignKey("SubIndustryID")]
        public long? SubIndustryID { get; set; }
        public virtual Industry Industry { get; set; }
    }
}
