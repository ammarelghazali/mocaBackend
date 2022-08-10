using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment.Base
{
    public class BaseWorkSpaceTailoredPricing : BaseEntity
    {
        [Required]
        public int HoursFrom { get; set; }

        [Required]
        public int HoursTo { get; set; }

        [Required]
        public decimal PricePerHour { get; set; }

        public decimal? VoucherPercentage { get; set; }

        public decimal? VoucherAmount { get; set; }
    }
}
