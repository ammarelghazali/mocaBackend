using MOCA.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.LocationManagment
{
    public class FloorBuilding : BaseEntity
    {
        public string Number { get; set; }
        [ForeignKey("BuildingID")]
        public long BuildingID { get; set; }
        public virtual Building Building { get; set; }
        public decimal GrossArea { get; set; }
        public decimal NetArea { get; set; }
        public int MaleRestroomCount { get; set; }
        public int FemaleRestroomCount { get; set; }
    }
}
