using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.PlanDtos.Response
{
    public class PlanDtoBase
    {
        public long? LobSpaceTypeId { get; set; }

        [Required]
        public long TypeId { get; set; }

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
