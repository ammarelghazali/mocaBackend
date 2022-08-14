using MOCA.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.DynamicLists
{
    public class FurnitureType: BaseEntity
    {
        [Required]
        public string Name { get; set; }

    }
}
