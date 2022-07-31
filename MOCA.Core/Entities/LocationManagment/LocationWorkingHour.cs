﻿using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class LocationWorkingHour : BaseEntity
    {
        [Required]
        public TimeSpan StartWorkingHour { get; set; }
        [Required]
        public TimeSpan EndWorkingHour { get; set; }
        [Required]
        public string DayFrom { get; set; }
        [Required]
        public string DayTo { get; set; }
        public long LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
    }
}
