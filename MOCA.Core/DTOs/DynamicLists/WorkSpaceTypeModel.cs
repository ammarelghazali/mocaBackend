﻿using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.DynamicLists
{
    public class WorkSpaceTypeModel
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }


        public long WorkSpaceCategoryId { get; set; }


    }
}
