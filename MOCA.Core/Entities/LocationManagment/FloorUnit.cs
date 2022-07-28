using MOCA.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.LocationManagment
{
    public class FloorUnit: BaseEntity
    {
        public string Name { get; set; }
        public long BuildingFloorId { get; set; }
        public virtual BuildingFloor BuildingFloor { get; set; }
        public decimal GrossArea { get; set; }
        public decimal NetArea { get; set; }
        public int MaleRestroomCount { get; set; }
        public int FemaleRestroomCount { get; set; }
    }
}
