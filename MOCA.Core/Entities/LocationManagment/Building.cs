﻿using MOCA.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.LocationManagment
{
    public class Building : BaseEntity
    {
        [ForeignKey("LocationtID")]
        public long LocationtID { get; set; }
        public virtual Location Location { get; set; }
        public string Name { get; set; }
        public decimal GrossArea { get; set; }
        public decimal LeasableArea { get; set; }
        public int MaleRestroomCount { get; set; }
        public int FemaleRestroomCount { get; set; }
    }
}
