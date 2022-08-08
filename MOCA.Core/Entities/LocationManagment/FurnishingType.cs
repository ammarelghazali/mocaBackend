using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.LocationManagment
{
    public class FurnishingType : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public ICollection<Furnishing> Furnishings { get; set; }
    }
}
