

using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.DynamicLists
{
    public class VenueSetupModel
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }

    }
}
