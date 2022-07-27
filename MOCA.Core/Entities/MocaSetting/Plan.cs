using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.MocaSetting
{
    public class Plan : BaseEntity
    {
        public long TypeId { get; set; }

        [ForeignKey("TypeId")]
        public PlanType PlanType { get; set; }

        public long? LobSpaceTypeId { get; set; }

        //[ForeignKey("LobSpaceTypeId")]
        //public LobSpaceType LobSpaceType { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public string Points { get; set; }

        [Required]
        public string WhatYouGet { get; set; }

        [Required]
        public string TermsOfUse { get; set; }

    }
}
