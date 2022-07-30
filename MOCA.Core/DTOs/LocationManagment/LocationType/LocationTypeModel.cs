using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.LocationManagment.LocationType
{
    public class LocationTypeModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
