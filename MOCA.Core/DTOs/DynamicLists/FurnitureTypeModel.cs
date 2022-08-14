
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.DynamicLists
{
    public class FurnitureTypeModel
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
