using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.LocationManagment.Industry
{
    public class IndustryModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
