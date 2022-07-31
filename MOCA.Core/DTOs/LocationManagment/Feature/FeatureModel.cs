using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.LocationManagment.Feature
{
    public class FeatureModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
