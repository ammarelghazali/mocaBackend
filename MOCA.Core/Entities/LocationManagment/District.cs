using MOCA.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.LocationManagment
{
    public class District : BaseEntity
    {
        public long CityId { get; set; }
        public virtual City City { get; set; }
        public string DistrictName { get; set; }
    }
}
