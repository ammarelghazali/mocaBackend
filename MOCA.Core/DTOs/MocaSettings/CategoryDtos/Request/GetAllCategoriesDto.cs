﻿using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.CategoryDtos.Request
{
    public class GetAllCategoriesDto
    {
        [Range(1, long.MaxValue, ErrorMessage = "Space Id Cannot Be 0")]
        public long? LobSpaceTypeId { get; set; }
        public bool WithFaqs { get; set; } = false;
        public bool WithNonCategorizedFaqs { get; set; } = false;
    }
}
