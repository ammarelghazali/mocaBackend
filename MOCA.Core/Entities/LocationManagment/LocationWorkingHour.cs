using MOCA.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.LocationManagment
{
    public class LocationWorkingHour : BaseEntity
    {
        public TimeSpan StartWorkingHour { get; set; }
        public TimeSpan EndWorkingHour { get; set; }
        public string DayFrom { get; set; }
        public string DayTo { get; set; }
        public long LocationID { get; set; }
        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }
    }
}
