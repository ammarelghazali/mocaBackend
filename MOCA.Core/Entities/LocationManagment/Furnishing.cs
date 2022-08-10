﻿using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class Furnishing : BaseEntity
    {

        [Required]
        public long FeatureId { get; set; }

        [ForeignKey("FeatureId")]
        public Feature Feature { get; set; }

        [Required]
        public long FurnishingTypeId { get; set; }

        [ForeignKey("FurnishingTypeId")]
        public FurnishingType FurnishingType { get; set; }
        public string? Vendor { get; set; }
        public string? Dimensions { get; set; }

        [Required]
        public string Specs { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string? SerialNumber { get; set; }

        public string? Picture { get; set; }

        [Required]
        public long SpaceId { get; set; }
    }
}