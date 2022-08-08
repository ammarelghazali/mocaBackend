using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.LocationManagment
{
    public class VenueSetup : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }
    }
}
