using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment.Base
{
    public class BaseWorkSpaceBundlePricing : BaseEntity
    {
        [Required]
        public string BundleName { get; set; }

        [Required]
        public int NumberOfUsers { get; set; }

        [Required]
        public int DurationInDays { get; set; } 

        [Required]
        public int NumberOfHours { get; set; }

        [Required]
        public decimal PricePerHour { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public DateTime Deactivation { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
