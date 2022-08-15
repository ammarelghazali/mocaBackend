
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.DynamicLists
{
    public class FurnishingTypeModel
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
