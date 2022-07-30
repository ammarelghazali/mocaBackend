using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.LocationManagment.Inclusion
{
    public class InclusionModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Icon { get; set; }
    }
}
