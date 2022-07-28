﻿using MOCA.Core.Entities.BaseEntities;
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
        public long LocationId { get; set; }
        public virtual Location Location { get; set; }
        public long? IndustryId { get; set; }
        public virtual Industry Industry { get; set; }
    }
}
